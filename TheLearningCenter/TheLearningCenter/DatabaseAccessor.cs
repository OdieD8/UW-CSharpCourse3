﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheLearningCenter
{
    public class DatabaseAccessor
    {
        private static readonly Entities entities;

        static DatabaseAccessor()
        {
            entities = new Entities();
            entities.Database.Connection.Open();
        }

        public static Entities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}