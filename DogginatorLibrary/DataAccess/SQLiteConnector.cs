using Dapper;
using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogginatorLibrary.DataAccess
{
    public class SQLiteConnector : IDataConnection
    {
        #region Fields
        /// <summary>
        /// Database Name
        ///</summary>
        private const string db = "Dogginator";
        
        #endregion

        #region Properties

        #endregion

        #region Contstructor

        #endregion

        #region Methods
        

        public DogModel AddDog(DogModel dModel, CustomerModel cModel)
        {
            AddDogToDatabase(dModel);
            AddDogToCustomer(dModel, cModel);
            Get_Customer(cModel);

            return dModel;
        }

        public void UpdateCustomer(CustomerModel cModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query(@"UPDATE Customer SET edit_date = datetime('now'),firstname = @Firstname, lastname = @LastName, street = @Street, 
                                       housenumber = @HouseNumber, zipcode = @ZipCode, city = @City, phonenumber = @PhoneNumber, mobilenumber = @MobileNumber, email = @Email WHERE id = @Id",cModel);
                }
            }

            catch (SQLiteException sqLiteEx)
            {
                Console.WriteLine(sqLiteEx.Message);
                
            }
        }

        public DogModel UpdateDog(DogModel dModel)
        {
            throw new NotImplementedException();
        }

        public List<CustomerModel> Get_CustomerAll()
        {
            List<CustomerModel> output;

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    output = connection.Query<CustomerModel>("SELECT * FROM CUSTOMER WHERE active = 1;").ToList();

                    foreach (CustomerModel customerModel in output)
                    {
                        customerModel.Notes = connection.Query<NoteModel>("SELECT n.* FROM note n INNER JOIN note_to_customer nc on noteId = n.id WHERE active = 1 AND customerId = " + customerModel.Id).ToList();
                        customerModel.OwnedDogs = connection.Query<DogModel>("SELECT d.* FROM dog d INNER JOIN customer_to_dog cd on dogId = d.id WHERE customerId = " + customerModel.Id).ToList();
                        foreach (DogModel dogModel in customerModel.OwnedDogs)
                        {
                            dogModel.Characteristics = connection.Query<CharacteristicsModel>("SELECT c.* FROM characteristics c INNER JOIN dog_to_characteristics dc on dc.id = c.id WHERE dogId = " + dogModel.Id).ToList();
                            dogModel.Diseases = connection.Query<DiseasesModel>("SELECT d.* FROM diseases d INNER JOIN dog_to_diseases dd on dd.id = d.id WHERE dogId = " + dogModel.Id).ToList();
                            dogModel.CustomerList = connection.Query<CustomerModel>("SELECT c.* FROM customer c INNER JOIN customer_to_dog cd on customerId = c.id WHERE dogId = " + dogModel.Id).ToList();
                        }
                    }
                }
                return output;
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);
                return new List<CustomerModel>();
            }
        }

        public void DeleteCustomer(CustomerModel model)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query("UPDATE Customer SET edit_date = datetime('now'), active = 0 WHERE id= " + model.Id);
                }
            }
            catch (SQLiteException sqLiteEx)
            {
                Console.WriteLine(sqLiteEx.Message);
                return;
            }
        }

        public CustomerModel AddCustomer(CustomerModel cModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    cModel.Id = connection.Query<int>(@"INSERT INTO Customer (salution, firstname, lastname, street, 
                                                        housenumber, zipcode, city, phonenumber, mobilenumber, email, 
                                                        birthday, create_date, edit_date,active) VALUES( 
                                                        @Salution, @FirstName, @LastName, @Street, @Housenumber, @ZipCode, 
                                                        @City, @PhoneNumber, @MobileNumber, @Email, @Birthday,  datetime('now'), 
                                                        null, 1); SELECT last_insert_rowid()", cModel).First();

                    if (cModel.Notes != null && cModel.Notes.Count > 0)
                    {
                        foreach (NoteModel nModel in cModel.Notes)
                        {
                            nModel.Id = connection.Query<int>(@"INSERT INTO note (description, active) VALUES (@Description, 1); SELECT last_insert_rowid()", nModel).First();
                            connection.Execute("INSERT INTO note_to_customer (customerId, noteId) Values(" + cModel.Id + ", " + nModel.Id + ")");
                        }
                    }

                    if (cModel.OwnedDogs != null && cModel.OwnedDogs.Count > 0)
                    {
                        foreach (DogModel dModel in cModel.OwnedDogs)
                        {
                            if (dModel.Id > 0)
                            {
                                AddDogToCustomer(dModel, cModel);
                            }
                            else
                            {
                                AddDogToDatabase(dModel);
                                AddDogToCustomer(dModel, cModel);
                            }

                        }
                    }
                }
                return cModel;
            }


            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<CustomerModel> GetAllCustomerForDog(DogModel dModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    dModel.CustomerList = connection.Query<CustomerModel>(@"SELECT c.* FROM customer c INNER JOIN customer_to_dog cd  on customerId = c.id WHERE dogId = @id",dModel).ToList();

                    return dModel.CustomerList;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CustomerModel>();
            }
        }

        public  DogModel AddDogToDatabase(DogModel dModel)
        {
            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                dModel.Id = connection.Query<int>(@"INSERT INTO Dog (name, breed, color, gender, birthday, tassoregistration,isChipped,
                                                                whichpoint, castrated,castratedsince, castratemethod, create_date, edit_date, active) 
                                                                VALUES(@Name, @Breed, @Color, @Gender, @Birthday, @TassoRegistration, @isChipped,
                                                                @WhichPoint, @Castrated, @CastratedSince, @CastrateMethod,  datetime('now'), null, 1 ); 
                                                                SELECT last_insert_rowid()", dModel).First();
                if (dModel.Diseases != null && dModel.Diseases.Count > 0)
                {
                    foreach (DiseasesModel disModel in dModel.Diseases)
                    {
                        disModel.Id = connection.Query<int>(@"INSERT INTO diseases (name, active) VALUES(@name, 1); SELECt last_insert_rowid()", disModel).First();
                        connection.Query("INSERT INTO dog_to_diseases (dogId, diseasesId) VALUES(" + dModel.Id + " ," + disModel.Id + ")");
                    }
                }

                if (dModel.Characteristics != null && dModel.Characteristics.Count > 0)
                {
                    foreach (CharacteristicsModel chaModel in dModel.Characteristics)
                    {
                        chaModel.Id = connection.Query<int>(@"INSERT INTO characteristics (description, active) VALUES (@Description, 1); SELECT last_insert_rowid()", chaModel).First();
                        connection.Query("INSERT INTO dog_to_characteristics (dogId, characteristicsId) VALUES(" + dModel.Id + " , " + chaModel.Id + ")");
                    }
                }
            }

            return dModel;
        }

        public void AddDogToCustomer(DogModel dModel, CustomerModel cModel)
        {
            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Query("INSERT INTO customer_to_dog (customerId, dogId) VALUES (" + cModel.Id + " ," + dModel.Id + ")");
            }
        }

        public List<DogModel> Get_DogsAll()
        {
            List<DogModel> output = new List<DogModel>();
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    output = connection.Query<DogModel>("SELECT * FROM Dog WHERE active = 1;").ToList();

                    foreach (DogModel dogModel in output)
                    {
                        dogModel.Characteristics = connection.Query<CharacteristicsModel>("SELECT c.* FROM characteristics c INNER JOIN dog_to_characteristics dc on dc.id = c.id WHERE active = 1 AND dogId = " + dogModel.Id).ToList();
                        dogModel.Diseases = connection.Query<DiseasesModel>("SELECT d.* FROM diseases d INNER JOIN dog_to_diseases dd on dd.id = d.id WHERE active = 1 AND dogId = " + dogModel.Id).ToList();
                        if (dogModel.CustomerList == null)
                        {
                            dogModel.CustomerList = new List<CustomerModel>();
                        }
                        dogModel.CustomerList = connection.Query<CustomerModel>("SELECT c.* FROM customer c INNER JOIN customer_to_dog cd on customerId = c.id WHERE dogId = " + dogModel.Id).ToList();

                    }
                }
                return output;
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);
                return new List<DogModel>();
            }
        }

        public NoteModel AddNoteToCustomer(CustomerModel cModel, string note)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    NoteModel nModel = new NoteModel();
                    nModel.Description = note;
                    nModel.Id = connection.Query<int>("INSERT INTO note (description, active) Values('" + note + "', 1); SELECT last_insert_rowid();").First();
                    connection.Query("UPDATE customer SET edit_date = datetime('now') WHERE customer.id= " + cModel.Id);
                    connection.Execute("INSERT INTO note_to_customer (customerId, noteId) Values(" + cModel.Id + ", " + nModel.Id + ")");
                    cModel.Notes = connection.Query<NoteModel>("SELECT n.* FROM note n INNER JOIN note_to_customer nc on noteId = n.id WHERE active = 1 AND customerId = " + cModel.Id).ToList();
                   
                    return nModel;
                }
               
            }
            catch (SQLiteException sqLiteEx)
            {
                Console.WriteLine(sqLiteEx.Message);
                return null;
            }
        }

        public CustomerModel Get_Customer(CustomerModel model)
        {
           
            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                model = connection.Query<CustomerModel>("SELECT * FROM CUSTOMER WHERE id = " + model.Id + ";").First();

                model.Notes = connection.Query<NoteModel>("SELECT n.* FROM note n INNER JOIN note_to_customer nc on noteId = n.id WHERE active = 1 AND customerId = " + model.Id).ToList();
                model.OwnedDogs = connection.Query<DogModel>("SELECT d.* FROM dog d INNER JOIN customer_to_dog cd on dogId = d.id WHERE customerId = " + model.Id).ToList();

                foreach (DogModel dogModel in model.OwnedDogs)
                {
                    dogModel.Characteristics = connection.Query<CharacteristicsModel>("SELECT c.* FROM characteristics c INNER JOIN dog_to_characteristics dc on dc.id = c.id WHERE dogId = " + dogModel.Id).ToList();
                    dogModel.Diseases = connection.Query<DiseasesModel>("SELECT d.* FROM diseases d INNER JOIN dog_to_diseases dd on dd.id = d.id WHERE dogId = " + dogModel.Id).ToList();
                }
                return model;
            }

        }

        public void DeleteNoteFromList(NoteModel noteModel, CustomerModel cModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query("UPDATE note SET active = 0  WHERE note.id = " + noteModel.Id + ";");
                    Get_Customer(cModel);
                }

            }
            catch (SQLiteException sqLiteEx)
            {
                Console.WriteLine(sqLiteEx.Message);
            }

        }

        public void DeleteDogToCustomerRelation(CustomerModel cModel, DogModel dModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query("DELETE FROM customer_to_dog WHERE customerId = " + cModel.Id + " AND dogId = " + dModel.Id);
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
