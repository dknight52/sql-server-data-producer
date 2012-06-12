﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLRepeater.Entities.Generators
{
    public sealed class RandomSupplier
    {
        static readonly RandomSupplier instance = new RandomSupplier();

        public static RandomSupplier Instance
        {
            get
            {
                return instance;
            }
        }

        static RandomSupplier()
        {        
        }

        RandomSupplier()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        public int GetNextInt()
        {
            return _random.Next();
        }
        public int GetNextInt(int min, int max)
        {
            return _random.Next(min, max);
        }
        public long GetNextLong()
        {
            //http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/2e08381b-1e2d-459f-a7c9-986954321958/
            return (long)((_random.NextDouble() * 2.0 - 1.0) * long.MaxValue);
        }
        public double GetNextDouble()
        {
            return _random.NextDouble();
        }
        
        readonly Random _random;
        
    }
}
