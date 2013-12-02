﻿//// Copyright 2012-2013 Peter Henell

////   Licensed under the Apache License, Version 2.0 (the "License");
////   you may not use this file except in compliance with the License.
////   You may obtain a copy of the License at

////       http://www.apache.org/licenses/LICENSE-2.0

////   Unless required by applicable law or agreed to in writing, software
////   distributed under the License is distributed on an "AS IS" BASIS,
////   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
////   See the License for the specific language governing permissions and
////   limitations under the License.

//using System;
//using SQLDataProducer.Entities.DatabaseEntities;
//using System.Xml.Serialization;
//using System.Linq;
//using System.Data;
//using System.Xml.Linq;
//using System.Collections.Generic;
//using SQLDataProducer.Entities.DataEntities.Collections;
//using SQLDataProducer.Entities.DataEntities;

//namespace SQLDataProducer.Entities.ExecutionEntities
//{

//    /// <summary>
//    /// An execution item is a table that have been configured to get data generated.
//    /// </summary>
//    public sealed class ExecutionNode : EntityBase//, IEquatable<ExecutionNode>
//    {
//        TableEntity _targetTable;
//        /// <summary>
//        /// The table to generate data for.
//        /// </summary>
//        public TableEntity TargetTable
//        {
//            get
//            {
//                return _targetTable;
//            }
//            private set
//            {
//                if (_targetTable != value)
//                {
//                    _targetTable = value;
//                    HasWarning = _targetTable.HasWarning;
//                    _targetTable.ParentExecutionItem = this;

//                    OnPropertyChanged("TargetTable");
//                }
//            }
//        }

//        int _order;
//        public int Order
//        {
//            get
//            {
//                return _order;
//            }
//            set
//            {
//                if (_order != value)
//                {
//                    _order = value;
//                    OnPropertyChanged("Order");
//                }
//            }
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="table">The table to generate data for</param>
//        /// <param name="order">the order of the execution item. Is used to generate the name of variables so that other execution items can depend on this</param>
//        public ExecutionNode(TableEntity table, string description = "")
//            : this()
//        {
//            if (table != null && table.ParentExecutionItem != null)
//                throw new InvalidOperationException("this table already belong to another execution item");

//            TargetTable = table;
//            Description = description;
//        }

//        public ExecutionNode()
//        {
//            Order = int.MinValue;
//        }


//        string _description;
//        /// <summary>
//        /// Description of the Execution Item. Use to describe the purpose of it
//        /// </summary>
//        public string Description
//        {
//            get
//            {
//                return _description;
//            }
//            set
//            {
//                if (_description != value)
//                {
//                    _description = value;
//                    OnPropertyChanged("Description");
//                }
//            }
//        }

//        bool _truncateBeforeExecution;
//        /// <summary>
//        /// Should the table be truncated before running the data generation?
//        /// </summary>
//        public bool TruncateBeforeExecution
//        {
//            get
//            {
//                return _truncateBeforeExecution;
//            }
//            set
//            {
//                if (_truncateBeforeExecution != value)
//                {
//                    _truncateBeforeExecution = value;
//                    OnPropertyChanged("TruncateBeforeExecution");
//                }
//            }
//        }


//        int _repeatExectution = 1;
//        public int RepeatCount
//        {
//            get
//            {
//                return _repeatExectution;
//            }
//            set
//            {
//                if (_repeatExectution != value)
//                {
//                    if (value < 1)
//                        value = 1;
//                    if (value > 1000)
//                        value = 1000;

//                    _repeatExectution = value;
//                    OnPropertyChanged("RepeatCount");
//                }
//            }
//        }


//        public ExecutionNode Clone()
//        {
//            // TODO: Clone using the same entity as the LOAD/SAVE functionality
//            TableEntity clonedTable = this.TargetTable.Clone();
//            var ei = new ExecutionNode(clonedTable);
//            ei.Description = this.Description;
//            ei.RepeatCount = this.RepeatCount;
//            ei.TruncateBeforeExecution = this.TruncateBeforeExecution;
//            ei.UseIdentityInsert = this.UseIdentityInsert;
//            return ei;
//        }
        

//        private ExecutionConditions _executionCondition = ExecutionConditions.None;
//        public ExecutionConditions ExecutionCondition
//        {
//            get
//            {
//                return _executionCondition;
//            }
//            set
//            {
//                _executionCondition = value;
//                OnPropertyChanged("ExecutionCondition");
//            }
//        }

//        private long _executionConditionValue;
//        public long ExecutionConditionValue
//        {
//            get
//            {
//                return _executionConditionValue;
//            }
//            set
//            {
//                _executionConditionValue = value;
//                OnPropertyChanged("ExecutionConditionValue");
//            }
//        }

//        public override string ToString()
//        {
//            return string.Format(@"this.Description = {0} 
//this.ExecutionCondition = '{1}', 
//this.ExecutionConditionValue = '{2}', 
//this.HasWarning = '{3}', 
//this.Order = '{4}', 
//this.RepeatCount = '{5}',
//this.TargetTable = '{6}',
//this.TruncateBeforeExecution = '{7}', 
//this.UseIdentityInsert = '{8}', 
//this.WarningText = '{9}' ",
//this.Description,
//this.ExecutionCondition,
//this.ExecutionConditionValue,
//this.HasWarning,
//this.Order,
//this.RepeatCount,
//this.TargetTable,
//this.TruncateBeforeExecution,
//this.UseIdentityInsert,
//this.WarningText);

//        }

//        private bool _hasWarning = false;
//        /// <summary>
//        /// This Execution Item have some kind of warning that might cause problems during execution
//        /// </summary>
//        public bool HasWarning
//        {
//            get
//            {
//                return _hasWarning;
//            }
//            set
//            {
//                _hasWarning = value;
//                OnPropertyChanged("HasWarning");
//            }
//        }

//        private string _warningText = string.Empty;
//        /// <summary>
//        /// Contains warning text if the this execution item have a warning that might cause problems during execution.
//        /// </summary>
//        public string WarningText
//        {
//            get
//            {
//                return _warningText;
//            }
//            set
//            {
//                _warningText = value;
//                OnPropertyChanged("WarningText");
//            }
//        }


//        /// <summary>
//        /// ExecutionItemCollection where this execution item is located.
//        /// </summary>
//        ExecutionItemCollection _parentCollection;
//        public ExecutionItemCollection ParentCollection
//        {
//            get
//            {
//                return _parentCollection;
//            }
//            set
//            {
//                if (_parentCollection != value)
//                {
//                    _parentCollection = value;
//                    OnPropertyChanged("ParentCollection");
//                }
//            }
//        }


//        bool _useIdenityInsert = false;
//        public bool UseIdentityInsert
//        {
//            get
//            {
//                return _useIdenityInsert;
//            }
//            set
//            {
//                if (_useIdenityInsert != value)
//                {
//                    _useIdenityInsert = value;
//                    OnPropertyChanged("UseIdentityInsert");
//                }
//            }
//        }

//        public bool ShouldExecuteForThisN(long N)
//        {
//            switch (ExecutionCondition)
//            {
//                case ExecutionConditions.None:
//                    return true;
//                case ExecutionConditions.LessThan:
//                    return ExecutionConditionValue < N;
//                case ExecutionConditions.LessOrEqualTo:
//                    return ExecutionConditionValue <= N;
//                case ExecutionConditions.EqualTo:
//                    return ExecutionConditionValue == N;
//                case ExecutionConditions.EqualOrGreaterThan:
//                    return ExecutionConditionValue >= N;
//                case ExecutionConditions.GreaterThan:
//                    return ExecutionConditionValue > N;
//                case ExecutionConditions.NotEqualTo:
//                    return ExecutionConditionValue != N;
//                case ExecutionConditions.EveryOtherX:
//                    if (N == 0)
//                        return true;
//                    return ExecutionConditionValue % N == 0;
//                default:
//                    throw new NotSupportedException("ExecutionConditions not supported");
//            }
//        }

//        public static DataRowSet CreatePreview(ExecutionNode ei)
//        {
//            long l = 0;
//            Func<long> getN = new Func<long>(() => { return l++; });

//            return ei.CreateData(getN, new SetCounter());
//        }


//        /// <summary>
//        /// Create set of data for one execution.
//        /// </summary>
//        /// <param name="getN"></param>
//        /// <returns></returns>
//        public DataRowSet CreateData(Func<long> getN, SetCounter insertCounter)
//        {
//            var dt = new DataRowSet();
//            dt.TargetTable = this.TargetTable;
//            dt.Order = this.Order;

//            long n = 0;
//            for (int i = 0; i < RepeatCount; i++)
//            {
//                n = getN();
//                //Console.WriteLine("Generating data with N = {0}", n);
//                if (ShouldExecuteForThisN(n))
//                {
//                    dt.Add(RowEntity.Create(TargetTable, n, i));
//                    insertCounter.Increment();
//                }
//            }

//            return dt;
//        }

//    }
//}
