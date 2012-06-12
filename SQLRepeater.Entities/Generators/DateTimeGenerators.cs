﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SQLRepeater.Entities.Generators.Collections;


namespace SQLRepeater.Entities.Generators
{
    public class DateTimeGenerator : GeneratorBase
    {
        private DateTimeGenerator(string name, ValueCreatorDelegate generator, GeneratorParameterCollection genParams)
            : base(name, generator, genParams)
        {
        }

        
        private static DateTime _currentDate = DateTime.Now;
        private static DateTime StartDate
        {
            get
            {
                return _currentDate;
            }
        }


        internal static ObservableCollection<GeneratorBase> GetGenerators()
        {
            // TODO: Add edge case date generators. 1ms after midgnight, 1ms before midnight etc.
            ObservableCollection<GeneratorBase> valueGenerators = new ObservableCollection<GeneratorBase>();
            valueGenerators.Add(CreateCurrentDateGenerator());
            valueGenerators.Add(CreateQueryGenerator());
            valueGenerators.Add(CreateDaysSeriesGenerator());
            valueGenerators.Add(CreateHoursSeriesGenerator());
            valueGenerators.Add(CreateMiliSecondSeriesGenerator());
            valueGenerators.Add(CreateMinutesSeriesGenerator());
            valueGenerators.Add(CreateRandomDateGenerator());
            valueGenerators.Add(CreateSecondSeriesGenerator());
            valueGenerators.Add(CreateSQLGetDateGenerator());
            
            return valueGenerators;
        }

        private static DateTimeGenerator CreateCurrentDateGenerator()
        {
            DateTimeGenerator gen = new DateTimeGenerator("Current Date", (n, p) =>
            {
                return Wrap(DateTime.Now.ToString());
            }
                , null);
            return gen;
        }

        private static DateTimeGenerator CreateStaticDateGenerator()
        {
            GeneratorParameterCollection paramss = new GeneratorParameterCollection();
            
            paramss.Add(new GeneratorParameter("DATE", DateTime.Now.ToString()));

            DateTimeGenerator gen = new DateTimeGenerator("Current Date", (n, p) =>
            {
                return Wrap(GetParameterByName(p, "DATE").ToString());
            }
                , paramss);
            return gen;
        }

        private static DateTimeGenerator CreateSQLGetDateGenerator()
        {
            DateTimeGenerator gen = new DateTimeGenerator("SQL GetDate()", (n, p) =>
            {
                return "Getdate()";
            }
                , null);
            return gen;
        }


        private static DateTimeGenerator CreateRandomDateGenerator()
        {
            DateTimeGenerator gen = new DateTimeGenerator("Random Date", (n, p) =>
            {
                return Wrap(StartDate.AddDays(RandomSupplier.Instance.GetNextInt() % 30));
            }
                , null);
            return gen;
        }

        private static DateTimeGenerator CreateSecondSeriesGenerator()
        {
            DateTimeGenerator gen = new DateTimeGenerator("Seconds Series", (n, p) =>
            {
                return Wrap(StartDate.AddSeconds(n));
            }
                , null);
            return gen;
        }

        private static DateTimeGenerator CreateMiliSecondSeriesGenerator()
        {
            DateTimeGenerator gen = new DateTimeGenerator("Miliseconds Series", (n, p) =>
            {
                return Wrap(StartDate.AddMilliseconds(n));
            }
                , null);
            return gen;
        }

        private static DateTimeGenerator CreateMinutesSeriesGenerator()
        {
            GeneratorParameterCollection paramss = new GeneratorParameterCollection();

            paramss.Add(new GeneratorParameter("Shift Seconds", 0));

            DateTimeGenerator gen = new DateTimeGenerator("Minutes Series", (n, p) =>
            {
                int shiftSeconds = int.Parse(GetParameterByName(p, "Shift Seconds").ToString());
                return Wrap(StartDate.AddMinutes(n).AddSeconds(shiftSeconds));
            }
                , paramss);
            return gen;
        }
        private static DateTimeGenerator CreateHoursSeriesGenerator()
        {
            DateTimeGenerator gen = new DateTimeGenerator("Hours Series", (n, p) =>
            {
                return Wrap(StartDate.AddHours(n));
            }
                , null);
            return gen;
        }
        private static DateTimeGenerator CreateDaysSeriesGenerator()
        {
            DateTimeGenerator gen = new DateTimeGenerator("Days Series", (n, p) =>
            {
                return Wrap(StartDate.AddHours(n));
            }
                , null);
            return gen;
        }
       



    }
}
