﻿using Exiled.API.Features;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace XpSystem
{
    public class XpDataSystem
    {
        public static List<PlayerXp> XpsRegistered = new();
        public static List<DataXpItem> DataXpItems = new();

        static readonly string dbFilePath = Main.Instance.Config.DatabaseDirectoryPath + "/" + Main.Instance.Config.DatabaseFileName + ".json";

        public static void SaveDatabase()
        {
            if (!Directory.Exists(Main.Instance.Config.DatabaseDirectoryPath))
                Directory.CreateDirectory(Main.Instance.Config.DatabaseDirectoryPath);

            if (XpsRegistered is null)
            {
                Log.Error("Error : La base de données est null !");
                return;
            }

            Log.Info("Xp Database Saving...");

            List<DataXpItem> itemsToSave = new();
            foreach (PlayerXp playerXp in XpsRegistered)
            {
                Log.Info("Saving Xp for " + playerXp.Player.Nickname);
                itemsToSave.Add(new(playerXp.Player.UserId, playerXp.Level, playerXp.Exp));
            }

            using (StreamWriter writer = new(dbFilePath))
            {
                var serializer = new SerializerBuilder().Build();
                writer.Write(serializer.Serialize(itemsToSave.ToArray()));
            }

            Log.Info("Xp Database Saved !");
        }

        public static void LoadDatabase()
        {
            XpsRegistered = new();
            DataXpItems = new();

            if (!Directory.Exists(Main.Instance.Config.DatabaseDirectoryPath))
            {
                Log.Info("Directory not found ! Creating...");
                Directory.CreateDirectory(Main.Instance.Config.DatabaseDirectoryPath);
            }

            if (!File.Exists(dbFilePath))
            {
                Log.Warn("Database File not found ! No Xp Loaded");
                return;
            }

            Log.Info("Xp Database Loading...");

            string dbJson = File.ReadAllText(dbFilePath);
            var deserializer = new DeserializerBuilder().Build();
            DataXpItem[] itemsLoaded = deserializer.Deserialize<DataXpItem[]>(dbJson);

            if (itemsLoaded is null)
            {
                Log.Error("Error : Null Database !");
                return;
            }

            foreach (DataXpItem item in itemsLoaded)
                DataXpItems.Add(new(item.UserId, item.Lvl, item.Exp));

            Log.Info("Xp Database Loaded for " + DataXpItems.Count + " players !");
        }
    }

    public class DataXpItem
    {
        public string UserId;
        public int Lvl;
        public int Exp;

        public DataXpItem(string adress, int lvl, int exp)
        {
            UserId = adress;
            Lvl = lvl;
            Exp = exp;
        }

        public DataXpItem() { }

        public override string ToString() => $"{UserId} - {Lvl} - {Exp}";
    }
}
