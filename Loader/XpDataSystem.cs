using Exiled.API.Features;
using System.Collections.Generic;
using System.IO;
using XpSystem.API.Features;
using XpSystem.Events.EventArgs;
using YamlDotNet.Serialization;

namespace XpSystem.Loader
{
    internal static class XpDataSystem
    {
        internal static List<PlayerXp> XpsRegistered = new();
        internal static List<DataXpItem> DataXpItems = new();

        static readonly string dbFilePath = Main.Instance.Config.DatabaseDirectoryPath + "/" + Main.Instance.Config.DatabaseFileName + ".json";

        internal static void ReloadDatabase()
        {
            SaveDatabase();
            LoadDatabase();
        }

        internal static void SaveDatabase()
        {
            SavingDatabaseEventArgs ev = new();
            Events.Handlers.Database.OnSavingDatabase(ev);

            if (!ev.IsAllowed) return;

            if (!Directory.Exists(Main.Instance.Config.DatabaseDirectoryPath))
                Directory.CreateDirectory(Main.Instance.Config.DatabaseDirectoryPath);

            if (XpsRegistered is null)
            {
                Log.Error("Error : Null Database !");
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

            Events.Handlers.Database.OnSavedDatabase();
        }

        internal static void LoadDatabase()
        {
            LoadingDatabaseEventArgs ev = new();
            Events.Handlers.Database.OnLoadingDatabase(ev);

            if (!ev.IsAllowed) return;

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

            Events.Handlers.Database.OnLoadedDatabase();
        }
    }

    public class DataXpItem
    {
        public string UserId;
        public int Lvl;
        public float Exp;

        public DataXpItem(string adress, int lvl, float exp)
        {
            UserId = adress;
            Lvl = lvl;
            Exp = exp;
        }

        public DataXpItem() { }

        public override string ToString() => $"{UserId} - {Lvl} - {Exp}";
    }
}