﻿using SupplyAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;

namespace SupplyAI
{
    public class Startup {
        public const string AppName = "SupplyAI";

        public static Database database { get; } = new Database();
        public static MongoClient client { get { return database.client; } }


        public static readonly DataSetDictionary Database = new DataSetDictionary();
        public static readonly TagDictionary Tags = new TagDictionary();

        public static void Init() {
            initTags(); //must be first
            initDatabase();
        }
        private static void initDatabase(){
            

            var ds = new DataSet() { Name = "Heart Arrhythmia" };
            ds.Summary = "This dataset contains studies of MRI scans of hearts from various adult male patients with hearth Arryhthmia.";
            ds.Authors.AddRange(new List<string>{ "Joe Smith", "Frank Zappa","Charlie Brown"});
            ds.addTags("heart", "disease");
            Database.Add(ds);
            ds = new DataSet() { Name = "Lung Cross-Section" };
            ds.Summary = "Contains anonymized patient x-rays of lungs with no abnormal conditions. Because the data was anonymized there was no information on patient age, gender or height.";
            ds.Authors.AddRange(new List<string> { "Frank Zappa", "Peter Peters" });
            ds.addTags("lungs","normal");
            Database.Add(ds);
            ds = new DataSet() { Name = "Motor Cortex Neural Activity" };
            ds.Authors.AddRange(new List<string> { "Joe Smith", "Peter Peters" });
            ds.addTags("brain","normal");      
            Database.Add(ds);

            ds = new DataSet() { Name = "Bone density scans in elderly patients " };
            ds.Authors.AddRange(new List<string> { "Steven Franks" });
            ds.addTags("age","disease","bones");
            Database.Add(ds);

            //copied and pasted
            ds = new DataSet() { Name = "Heart Arrhythmia" };
            ds.Summary = "This dataset contains studies of MRI scans of hearts from various adult male patients with hearth Arryhthmia.";
            ds.Authors.AddRange(new List<string> { "Joe Smith", "Frank Zappa", "Charlie Brown" });
            ds.addTags("heart", "disease");
            Database.Add(ds);
            ds = new DataSet() { Name = "Lung Cross-Section" };
            ds.Summary = "Contains anonymized patient x-rays of lungs with no abnormal conditions. Because the data was anonymized there was no information on patient age, gender or height.";
            ds.Authors.AddRange(new List<string> { "Frank Zappa", "Peter Peters" });
            ds.addTags("lungs", "normal");
            Database.Add(ds);
            ds = new DataSet() { Name = "Motor Cortex Neural Activity" };
            ds.Authors.AddRange(new List<string> { "Joe Smith", "Peter Peters" });
            ds.addTags("brain", "normal");
            Database.Add(ds);
            ds = new DataSet() { Name = "Bone density scans in elderly patients " };
            ds.Authors.AddRange(new List<string> { "Steven Franks" });
            ds.addTags("age", "disease", "bones");
            Database.Add(ds);

        }


        private static void initTags() {
            at("organ");
            at("organ:bones");
            at("organ:heart");
            at("organ:skin");
            at("organ:lungs");
            at("organ:brain");
            at("condition");
            at("condition:normal");
            at("condition:disease");
            at("anatomical region");
            at("patient");
            at("patient:age");
            at("patient:gender");
            at("patient:weight");
            at("patient:ethnicity");
            at("size");
        }
        //a shortcut method for adding Tags (at=> add tag)
        private static void at(string name) {
            SupplyAI.Models.Tag parent = null;
            name = name.ToLower(); //only lowercase letters for Tags
            if (name.Contains(':')) { 
                string parentName = name.Substring(0, name.IndexOf(':'));
                name = name.Substring(name.IndexOf(':')+1);
                parent = Tags[parentName];
                if (parent == null)
                    throw new Exception("Parent Tag Named {"+parentName+"} does not exist."); //replace this with a browser side warning when trying to add a tag that doesn't work
            }

            Tags.Add(new SupplyAI.Models.Tag(name,parent));
        }
    }
}