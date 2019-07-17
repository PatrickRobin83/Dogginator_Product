using Dapper;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.DogginatorLibrary.DataAccess
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

        #region Customer

        public void UpdateCustomer(CustomerModel cModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query(@"UPDATE Customer SET edit_date = datetime('now'),firstname = @Firstname, lastname = @LastName, street = @Street, birthday = @Birthday,
                                       housenumber = @HouseNumber, zipcode = @ZipCode, city = @City, phonenumber = @PhoneNumber, mobilenumber = @MobileNumber, email = @Email, 
                                        active = @Active WHERE id = @Id", cModel);
                }
            }

            catch (SQLiteException sqLiteEx)
            {
                Console.WriteLine(sqLiteEx.Message);

            }
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
                    dModel.CustomerList = connection.Query<CustomerModel>(@"SELECT c.* FROM customer c INNER JOIN customer_to_dog cd  on customerId = c.id WHERE dogId = @id", dModel).ToList();

                    return dModel.CustomerList;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CustomerModel>();
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

        public List<CustomerModel> Get_CustomerInactiveAndActive()
        {
            List<CustomerModel> output;

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    output = connection.Query<CustomerModel>("SELECT * FROM CUSTOMER;").ToList();

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

        public List<CustomerModel> SearchResultsCustomer(string searchText, bool activeAndInactive)
        {
            List<CustomerModel> results = new List<CustomerModel>();

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    if (activeAndInactive)
                    {
                        results = connection.Query<CustomerModel>($"SELECT * FROM customer where firstname like '%{searchText}%' OR lastname like '%{searchText}%' OR Street like '%{searchText}%'" +
                        $"OR housenumber like '%{searchText}%' OR zipcode like '%{searchText}%' OR city like '%{searchText}%' OR phonenumber like '%{searchText}%' OR " +
                        $"mobilenumber like '%{searchText}%' OR email like '%{searchText}%' or birthday like '%{searchText}%'").ToList();
                    }
                    else
                    {
                        results = connection.Query<CustomerModel>($"SELECT * FROM customer where (firstname like '%{searchText}%' OR lastname like '%{searchText}%' OR Street like '%{searchText}%'" +
                        $"OR housenumber like '%{searchText}%' OR zipcode like '%{searchText}%' OR city like '%{searchText}%' OR phonenumber like '%{searchText}%' OR " +
                        $"mobilenumber like '%{searchText}%' OR email like '%{searchText}%' or birthday like '%{searchText}%') AND active = 1").ToList();
                    }

                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);

            }
            return results;
        }

        public void DeleteNoteFromList(NoteModel noteModel, CustomerModel cModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query("UPDATE note SET active = 0  WHERE note.id = " + noteModel.Id + ";");
                    Get_Customer(cModel);
                    UpdateCustomer(cModel);
                }

            }
            catch (SQLiteException sqLiteEx)
            {
                Console.WriteLine(sqLiteEx.Message);
            }

        }

        #region CityToZipcode

        public List<string> getCityToZipcode(string zipCode)
        {
            List<string> foundcitys = new List<string>();
            string id_zip;
            List<string> id_city = new List<string>();

            try
            {
                using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    if (connection.Query<string>($"Select id from zipcode where zip = '{zipCode}'").Any())
                    {
                        id_zip = connection.Query<string>($"Select id from zipcode where zip = '{zipCode}'").First();
                        id_city = connection.Query<string>($"Select id_city from ziptocity where id_zip = '{id_zip}'").ToList();
                        foreach (string cityZip in id_city)
                        {
                            foundcitys.Add(connection.Query<string>($"Select name from city where id = '{cityZip}'").First());
                        }

                        foundcitys.Sort();
                    }
                }
                    
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ErrorCode);
            }

            return foundcitys;
        }

        #endregion

        #endregion

        #region Dog
        /// <summary>
        /// Inserts the Dog into the Database
        /// Inserts the DogToCustomer Relation into the Database
        /// Add the Customer to the DogModel Customer List
        /// </summary>
        /// <returns>
        /// Dog complete with id 
        /// </returns>
        public DogModel AddDog(DogModel dModel, CustomerModel cModel)
        {
            AddDogToDatabase(dModel);
            AddDogToCustomer(dModel, cModel);
            Get_Customer(cModel);

            return dModel;
        }
        /// <summary>
        /// Updates a Dog in the Database with new Attributes
        /// </summary>
        public void UpdateDog(DogModel dModel)
        {

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query(@"UPDATE Dog SET edit_date = datetime('now'), name = @Name, breed = @Breed, color = @Color, gender = @Gender, birthday = @Birthday, 
                                       permanentcastrated = @PermanentCastrated, 
                                       castratedsince = @CastratedSince, effectiveuntil = @EffectiveUntil, active = @Active  WHERE id = @Id", dModel);
                }
            }

            catch (SQLiteException sqLiteEx)
            {
                Console.WriteLine(sqLiteEx.Message + "\r\n" + sqLiteEx.StackTrace);
            }


        }
        /// <summary>
        /// Inserts the Dog with Characteristics and Diseases in the Database
        /// </summary>
        /// <param name="dModel">Dogmodel to save</param>
        /// <returns>Dogmodel with the ID from the Database</returns>
        public DogModel AddDogToDatabase(DogModel dModel)
        {
            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                dModel.Id = connection.Query<int>(@"INSERT INTO Dog (name, breed, color, gender, birthday,permanentcastrated, castratedsince, effectiveuntil, create_date, edit_date, active) VALUES(@Name, @Breed, @Color, @Gender, @Birthday, @PermanentCastrated, @CastratedSince, @EffectiveUntil, datetime('now'), null, 1 ); SELECT last_insert_rowid();", dModel).First();
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
        /// <summary>
        /// Inserts a DogToCustomer Relation in the Database.
        /// </summary>
        /// <param name="dModel">DogModel ID which is needed to save the relation in Database</param>
        /// <param name="cModel">CustomerModel ID which is needed to save the relation in the Database</param>
        public void AddDogToCustomer(DogModel dModel, CustomerModel cModel)
        {
            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Query("INSERT INTO customer_to_dog (customerId, dogId) VALUES (" + cModel.Id + " ," + dModel.Id + ")");
            }
        }
        /// <summary>
        /// Selects all Dogs in the Database inactive and active
        /// </summary>
        /// <returns>List of DogModels</returns>
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
        /// <summary>
        /// Deletes the DogToCustomer relation entry in the Database
        /// </summary>
        /// <param name="cModel">CutomerModel needed for the deletion</param>
        /// <param name="dModel">DogModel needed for the deletion</param>
        public void DeleteDogToCustomerRelation(CustomerModel cModel, DogModel dModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query("DELETE FROM customer_to_dog WHERE customerId = " + cModel.Id + " AND dogId = " + dModel.Id);
                    UpdateDog(dModel);
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Deletes the Relation Between Dog and Disease in the Database
        /// </summary>
        /// <param name="dModel">DogModel that holds a List of its own disease</param>
        public void DeleteDogDiseasesRelation(DogModel dModel)
        {
            try
            {
                if (dModel.Diseases != null && dModel.Diseases.Count > 0)
                {
                    using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                    {
                        foreach (DiseasesModel d in dModel.Diseases)
                        {
                            connection.Query("DELETE FROM dog_to_diseases WHERE dogId = " + dModel.Id + " AND diseasesId = " + d.Id);
                            DeleteDiseases(d);
                            UpdateDog(dModel);
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Deletes the Relation Between Dog and Characteristics in the Database
        /// </summary>
        /// <param name="dModel">Dogmodel that holds a List of its own characterisitcs</param>
        public void DeleteDogToCharacteristicsRelation(DogModel dModel)
        {
            try
            {
                if (dModel.Characteristics != null && dModel.Characteristics.Count > 0)
                {
                    using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                    {
                        foreach (CharacteristicsModel c in dModel.Characteristics)
                        {
                            connection.Query("DELETE FROM dog_to_characteristics WHERE dogId = " + dModel.Id + " AND characteristicsId = " + c.Id);
                            DeleteCharacteristics(c);
                            UpdateDog(dModel);
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Deletes the Diseases from the database
        /// </summary>
        /// <param name="diseasesModel">Disease Model to delete from database</param>
        public void DeleteDiseases(DiseasesModel diseasesModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query("DELETE FROM diseases WHERE id = " + diseasesModel.Id);
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Deletes the given Characteristic from the database
        /// </summary>
        /// <param name="characteristicsModel">Characteristics Model to delete from database</param>
        public void DeleteCharacteristics(CharacteristicsModel characteristicsModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query("DELETE FROM characteristics WHERE id = " + characteristicsModel.Id);
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Delete the given Dog from the Database
        /// </summary>
        /// <param name="dogModel">DogModel to delete from database</param>
        public void DeleteDogFromDatabase(DogModel dogModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query("UPDATE dog SET edit_date = datetime('now'), active = 0 WHERE id= " + dogModel.Id);
                }
            }
            catch (SQLiteException sqLiteEx)
            {
                Console.WriteLine(sqLiteEx.Message);
                return;
            }
        }
        /// <summary>
        /// Selects all inactive and active dogs from the database and return the result as a List
        /// </summary>
        /// <returns>List<DogModel></returns>
        public List<DogModel> Get_DogsInactiveAndActive()
        {
            List<DogModel> output = new List<DogModel>();
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    output = connection.Query<DogModel>("SELECT * FROM Dog ;").ToList();

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
        /// <summary>
        /// Searches in the Table Dogs for the given Text in every column
        /// </summary>
        /// <param name="searchText">string of the searchtext that was entered in the Textbox</param>
        /// <param name="activeAndInactive">bool if searched only for active dogs or active and inactive dogs</param>
        /// <returns>Returns a List of Dogmodel that  matches the searchtext</returns>
        public List<DogModel> SearchResultDogs(string searchText, bool activeAndInactive)
        {
            List<DogModel> results = new List<DogModel>();

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    if (activeAndInactive)
                    {
                        results = connection.Query<DogModel>($"SELECT * FROM dog WHERE name like '%{searchText}%' OR breed like '%{searchText}%' OR " +
                            $"color like '%{searchText}%' OR gender like '%{searchText}%' OR birthday like '%{searchText}%'").ToList();
                    }
                    else
                    {
                        results = connection.Query<DogModel>($"SELECT * FROM dog WHERE (name like '%{searchText}%' OR breed like '%{searchText}%' OR " +
                            $"color like '%{searchText}%' OR gender like '%{searchText}%' OR birthday like '%{searchText}%') AND active = 1").ToList();
                    }
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);

            }
            return results;

        }
        /// <summary>
        /// Selects a DogModel from the Database
        /// </summary>
        /// <param name="id">DogModel id from the entry in the database</param>
        /// <returns>a complete Dogmodel out of the database</returns>
        public DogModel GetDog(int id)
        {
            DogModel dogModel = new DogModel();

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    dogModel = connection.Query<DogModel>($"SELECT * FROM Dog WHERE id = {id};").First();
                }
            }
            catch(SQLiteException e)
            {
                Console.WriteLine(e.Message);
            }

                    return dogModel;
        }

        #endregion

        #region User
        /// <summary>
        /// checks database for user password combination
        /// </summary>
        /// <param name="userModel">UserModel that holds the username and password</param>
        /// <returns>true if the combination is right - false if the combination is wrong</returns>
        public UserModel IsUserAndPasswordRight(UserModel userModel)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    if (connection.Query<UserModel>("SELECT * FROM user WHERE username = '" + userModel.Username + "';").ToList().Count > 0)
                    {
                        userModel = connection.Query<UserModel>("SELECT * FROM user WHERE username = '" + userModel.Username + "';").First();
                    }
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);
            }
            return userModel;
        }
        /// <summary>
        /// Inserts a user into the user table in the database
        /// </summary>
        /// <param name="userModel"></param>
        public void InsertUserToDatabase(UserModel userModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query(@"INSERT INTO user (username, password, isadmin,isactive,create_date) VALUES(@Username, @Password, @IsAdmin,1, datetime('now') );", userModel);
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);
            }
        }
        /// <summary>
        /// Selects all active user from the database
        /// </summary>
        /// <returns>Returns a List of UserModel</returns>
        public List<UserModel> GetAllActiveUser()
        {
            List<UserModel> output = new List<UserModel>();

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    output = connection.Query<UserModel>("SELECT * FROM user where isactive = 1").ToList();
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);
            }
            return output;
        }
        /// <summary>
        /// Deletes the given user from the database
        /// </summary>
        /// <param name="userModel"></param>
        public void DeleteUserFromDataBase(UserModel userModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query($" UPDATE user SET edit_date = datetime('now'), isactive = 0 WHERE {userModel.Id} = id");
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);
            }
        }
        /// <summary>
        /// Selects all Users from Database active also as inactive
        /// </summary>
        /// <returns>List of Users</returns>
        public List<UserModel> GetAllUser()
        {
            List<UserModel> output = new List<UserModel>();

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    output = connection.Query<UserModel>("SELECT * FROM user").ToList();
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);
            }
            return output;
        }
        /// <summary>
        /// Updates the values of the given user in the database
        /// </summary>
        /// <param name="userModel">User to update</param>
        public void UpdateUser(UserModel userModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query($" UPDATE user SET edit_date = datetime('now'), isactive = {userModel.IsActive}, username = '{userModel.Username}' , password = '{userModel.Password}', isadmin = {userModel.IsAdmin} WHERE '{userModel.Id}' = id");
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);
            }
        }
        /// <summary>
        /// Searches in the Table user for the given Text in every column
        /// </summary>
        /// <param name="searchText">Searches in the Table Dogs for the given Text in every column</param>
        /// <param name="showInactive">bool if searched only for active users or active and inactive users </param>
        /// <returns></returns>
        public List<UserModel> SearchResultUser(string searchText, bool showInactive)
        {
            List<UserModel> output = new List<UserModel>();

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    if (showInactive)
                    {
                        output = connection.Query<UserModel>($"SELECT * FROM user WHERE username like '%{searchText}%'").ToList();
                    }
                    else
                    {
                        output = connection.Query<UserModel>($"SELECT * FROM user WHERE username like '%{searchText}%' AND isactive = 1").ToList();
                    }
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);

            }

            return output;
        }
        #endregion

        #region Product
        /// <summary>
        /// Selects all Products from the database and writes it into a List of ProductModel 
        /// </summary>
        /// <returns>List of ProductModel</returns>
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    products = connection.Query<ProductModel>("SELECT * FROM product WHERE active = 1;").ToList();

                    return products;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ProductModel>();
            }
        }
        /// <summary>
        /// Adds a product into the table product from the database
        /// </summary>
        /// <param name="productModel">ProductModel to add</param>
        /// <returns>given ProductModel with id from database table</returns>
        public ProductModel AddProductToDatabase(ProductModel productModel)
        {
            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                productModel.ItemNumber = connection.Query<int>($"INSERT INTO product (shortdescription, longdescription, price, active, create_date, edit_date) VALUES ('{productModel.Shortdescription}', '{productModel.Longdescription}', '{productModel.Price}', {productModel.Active}, datetime('now'), null); SELECT last_insert_rowid();", productModel).First();

                return productModel;
            }
        }
        /// <summary>
        /// Deletes the product from the table product in database
        /// </summary>
        /// <param name="productModel">ProductModel to delete</param>
        public void DeleteProductFromDatabase(ProductModel productModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query("UPDATE product SET edit_date = datetime('now'), active = 0 WHERE itemnumber = " + productModel.ItemNumber);
                }
            }
            catch (SQLiteException sqLiteEx)
            {
                Console.WriteLine(sqLiteEx.Message);
                return;
            }
        }
        /// <summary>
        /// Updates the values from the ProductModel in the database with the values of the given ProductModel
        /// </summary>
        /// <param name="productModel">Product to update</param>
        public void UpdateProduct(ProductModel productModel)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    connection.Query($"UPDATE product SET edit_date = datetime('now'), shortdescription = '{productModel.Shortdescription}', longdescription = '{productModel.Longdescription}', price = '{productModel.Price}', active = {productModel.Active} WHERE '{productModel.ItemNumber}' = itemnumber ");
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);
            }
        }
        /// <summary>
        /// Returns a List with ProductModel that matches the searchstring from the textbox and all or only the active products
        /// </summary>
        /// <param name="searchText">string from the TextBox </param>
        /// <param name="showInactive">bool only active or also inactive products</param>
        /// <returns>List of products that matches the search string and the bool</returns>
        public List<ProductModel> SearchResulProducts(string searchText, bool showInactive)
        {
            List<ProductModel> output = new List<ProductModel>();

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    if (showInactive == true)
                    {
                        output = connection.Query<ProductModel>($"SELECT * FROM product WHERE shortdescription like '%{searchText}%' or longdescription like '%{searchText}%' or itemnumber like '%{searchText}%'").ToList();
                    }
                    else
                    {
                        output = connection.Query<ProductModel>($"SELECT * FROM product WHERE shortdescription like '%{searchText}%' AND active = 1 or longdescription like '%{searchText}%' AND active = 1 or itemnumber like '%{searchText}%' AND active = 1").ToList();
                    }
                }
            }
            catch (SQLiteException sqEx)
            {
                Console.WriteLine(sqEx.Message);

            }

            return output;
        }

        #endregion

        #region Appointment

        public List<AppointmentModel> getAppointmentsForDog(DogModel dogModel)
        {
            List<AppointmentModel> appointments = new List<AppointmentModel>();

            try
            {
                using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    appointments = connection.Query<AppointmentModel>($"Select * FROM appointment WHERE {dogModel.Id} = dogId").ToList();

                    return appointments;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AppointmentModel>();
            }

        }

        public AppointmentModel AddAppointmentToDatabase(AppointmentModel appointmentModel)
        {
            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                
                {
                    appointmentModel.Id = connection.Query<int>($"INSERT INTO appointment (dogID, date_from, date_to, isdailyguest, days) VALUES ('{appointmentModel.dogFromCustomer.Id}','{appointmentModel.date_from}','{appointmentModel.date_to}','{appointmentModel.IsDailyGuest}','{appointmentModel.days}'); SELECT last_insert_rowid();", appointmentModel).First();
                }
            }
            return appointmentModel;
        }

        public bool isAppointmentInDatabase(AppointmentModel appointmentModel)
        {
            bool isInDatabase = false;

            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                List<AppointmentModel> AppointmentModelList = new List<AppointmentModel>();
                AppointmentModelList = connection.Query<AppointmentModel>($"Select * FROM appointment WHERE '{appointmentModel.dogFromCustomer.Id}' = dogId AND " +
                                                                          $"date_from = '{appointmentModel.date_from}' AND " +
                                                                          $"date_to = '{appointmentModel.date_to}'").ToList();
                if (AppointmentModelList.Count >= 1)
                {
                    isInDatabase = true;
                }
            }
                return isInDatabase;
        }

        public bool isDogInTimeSpanAlreadyInDatabase(AppointmentModel appointmentModel)
        {
            bool isInDatabase = false;

            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                List<AppointmentModel> AppointmentModelList = new List<AppointmentModel>();
                AppointmentModelList = connection.Query<AppointmentModel>($"Select * FROM appointment WHERE '{appointmentModel.dogFromCustomer.Id}' = dogId AND " +
                                                                          $"'{appointmentModel.date_from}' BETWEEN date_from AND date_to").ToList();
                if (AppointmentModelList.Count >= 1)
                {
                    isInDatabase = true;
                }
            }

            return isInDatabase;
        }

        public List<AppointmentModel> getAppointments()
        {
            List<AppointmentModel> appointments = new List<AppointmentModel>();
            try
            {
                using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
                {
                    appointments = connection.Query<AppointmentModel>($"SELECT * FROM appointment").ToList();
                }
                
            }
            catch (SQLiteException ex)
            {

                Console.WriteLine(ex.Message);
            }
           foreach (AppointmentModel model in appointments)
           {
                model.dogFromCustomer = GetDog(model.DogId);
                model.dogFromCustomer.CustomerList = GetAllCustomerForDog(GetDog(model.DogId));
           }
            return appointments;
        }

        #endregion

        #endregion
    }
}