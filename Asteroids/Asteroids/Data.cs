using Newtonsoft.Json;

namespace Asteroids
{
    public class GameData
    {
        private static readonly string DataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        private static readonly string FilePath = Path.Combine(DataFolder, "ship_data.json");

        public ShipData LoadShipData()
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("No save file found - using default values");
                return null;
            }
            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<ShipData>(json);
            
        }

        public void SaveShipData()
        {

            var ship = new ShipData
            {
                Position = new ShipPosition
                {
                    X = (int)Player.playerPosition.X,
                    Y = (int)Player.playerPosition.Y
                },
                Rotation = new ShipRotation
                {
                    PlayerRotation = Player.playerRotation
                }
            };

            if (!Directory.Exists(DataFolder))
            {
                Console.WriteLine(FilePath, " created");
                Directory.CreateDirectory(DataFolder);
            }

            string json = JsonConvert.SerializeObject(ship, Formatting.Indented);
            File.WriteAllText(FilePath, json);
            Console.WriteLine("Game state saved successfully");

        }
    }

    public class ShipData
    {
        public ShipPosition Position { get; set; }
        public ShipRotation Rotation { get; set; }
    }

    public class ShipPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class ShipRotation
    {
        public float PlayerRotation { get; set; }
    }
}