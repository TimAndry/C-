using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LostInTheWoods.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace LostInTheWoods.Factories {
    public class TrailsFactory {
        private MySqlOptions _options;

        public TrailsFactory(IOptions<MySqlOptions> config){
            _options = config.Value;
        }
        internal IDbConnection Connection {
            get {
                return new MySqlConnection (_options.ConnectionString);
            }
        }

        //TRAILSFACTORY CLASS DEFINITION
        public void Add (Trail newTrail) {
            using (IDbConnection dbConnection = Connection) {
                var query = $"INSERT INTO TRAILS(TrailName, TrailLength, TrailDescription, ElevationChange) VALUES (@TrailName, @TrailLength, @TrailDescription, @ElevationChange)";
                dbConnection.Open ();
                dbConnection.Execute (query, newTrail);
                System.Console.WriteLine("****************************The trail was added successfully*****************************");
            }
        }
        public IEnumerable<Trail> FindAll () {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open ();
                return dbConnection.Query<Trail> ("SELECT * FROM trails ORDER BY TrailLength ASC");
            }
        }
        public Trail FindByID (int id) {
            using (IDbConnection dbConnection = Connection) {

                dbConnection.Open ();
                return dbConnection.Query<Trail> ("SELECT * FROM trails WHERE idTrails = @Id", new { Id = id }).FirstOrDefault ();

            }
        }
    }
}