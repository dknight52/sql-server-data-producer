﻿using SQLDataProducer.Entities.DatabaseEntities;
using SQLDataProducer.Entities.DataEntities;
using SQLDataProducer.Entities.ExecutionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLDataProducer.DataConsumers.DataToMSSSQLInsertionConsumer
{
    public class TableQueryGenerator
    {
        private readonly Entities.DatabaseEntities.TableEntity table;
        private readonly string columnList;
        private readonly string insertStatement;

        public string ColumnList
        {
            get { return columnList; }
        }
       
        public string InsertStatement
        {
            get { return insertStatement; }
        }

        public TableQueryGenerator(Entities.DatabaseEntities.TableEntity table)
        {
            this.table = table;
            this.columnList = string.Join(", ", table.Columns.Where(x => x.IsIdentity == false).Select(x => x.ColumnName));
            this.insertStatement = "INSERT INTO " + table.TableName + "(" + columnList + ")";
        }

        public string GenerateValuesStatement(DataRowEntity row, ValueStore valueStore)
        {
            return "VALUES (" +
                    string.Join(", ", row.Fields
                    .Where(x => x.ProducesValue == false)
                    .Select( x => string.Format(x.DataType.StringFormatter, valueStore.GetByKey(x.KeyValue))))
                    + ")";
        }

        /// <summary>
        /// To cache the query generators, so that we dont need to create a new one for each time the same table occurs.
        /// TODO: How to clean this up?
        /// </summary>
        private static Dictionary<string, TableQueryGenerator> tableQueryGenerators;
        static TableQueryGenerator()
        {
            tableQueryGenerators = new Dictionary<string, TableQueryGenerator>();
        }

        private static TableQueryGenerator GetQueryGeneratorFor(TableEntity table)
        {
            TableQueryGenerator queryGenerator;
            if (!tableQueryGenerators.TryGetValue(table.FullName, out queryGenerator))
            {
                queryGenerator = new TableQueryGenerator(table);
                tableQueryGenerators[table.FullName] = queryGenerator;
            }
            return queryGenerator;
        }

        public static IEnumerable<string> GenerateInsertStatements(IEnumerable<DataRowEntity> rows, ValueStore valueStore)
        {
            foreach (var row in rows)
            {
                yield return GetQueryGeneratorFor(row.Table).GenerateInsertStatement(row, valueStore);
            }
        }

        public string GenerateInsertStatement(DataRowEntity row, ValueStore valueStore)
        {
            return insertStatement + " " + GenerateValuesStatement(row, valueStore);
        }
    }
}
