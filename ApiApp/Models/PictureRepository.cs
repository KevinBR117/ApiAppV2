using SQLite;

namespace ApiApp.Models
{
    public class PictureRepository
    {
        string _dbName;
        public string StatusMessage { get; set; }
        public string estadoConexion { get; set; }
        private SQLiteAsyncConnection conx;

        private async Task Init()
        {
            if (conx != null)
                return;

            string dbPath = Path.Combine(Constants.DatabasePath, _dbName);
            try
            {
                //Resulta que las flags si son indispensables, de lo contrario el primer
                //intento de grabar datos, falla, hasta el segundo funciona
                conx = new SQLiteAsyncConnection(dbPath, Constants.Flags);
                if (conx == null)
                    throw new Exception("No logra abrir conexión a BD");

                estadoConexion = "se logro abrir la conexion ala BD";
                await conx.CreateTableAsync<Picture>();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("No se logró crear la tabla. {0}", ex.Message);
            }
        }

        public PictureRepository(string dbName)
        {
            _dbName = dbName;
        }

        public async Task AddNewPictureAsync(string name)
        {
            int result = 0;
            try
            {
                await Init();

                //validacion para asegurarse que el nombre fue ingresado
                if (string.IsNullOrEmpty(name))
                    throw new Exception("valid name is required");

                result = await conx.InsertAsync(new Picture { Name = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("failed to add {0}. Error: {1}", name, ex.Message);
            }
        }

        public async Task<List<Picture>> GetAllPictures()
        {
            try
            {
                await Init();
                return await conx.Table<Picture>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("failed to retrive data. {0}", ex.Message);
            }

            return new List<Picture>();
        }

        public static class Constants
        {
            public const SQLite.SQLiteOpenFlags Flags =
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache;

            public static string DatabasePath
            {
                get
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                }
            }
        }

    }
}
