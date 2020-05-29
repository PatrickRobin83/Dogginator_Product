/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   IDataConnection.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using System.Collections.Generic;


namespace de.rietrob.dogginator_product.DogginatorLibrary.DataAccess
{
    public interface IDataConnection
    {
        #region Customer

        /// <summary>
        /// Updates the given Customer in the data store
        /// </summary>
        /// <param name="cModel">CustomerModel to update</param>
        void UpdateCustomer(CustomerModel cModel);

        /// <summary>
        /// Selects all Customers from the data store and stores them in a List of CustomerModels
        /// </summary>
        /// <returns>List of CustomerModel</returns>
        List<CustomerModel> Get_CustomerAll();

        /// <summary>
        /// Deletes the given CustomerModel from the data store
        /// </summary>
        /// <param name="customerModel">CustomerModel to delete</param>
        void DeleteCustomer(CustomerModel model);

        /// <summary>
        /// Adds the given CustomerModel into the data store
        /// </summary>
        /// <param name="customerModel">CustomerModel to add</param>
        /// <returns>CustomerModel with the id from the data store </returns>
        CustomerModel AddCustomer(CustomerModel cModel);

        /// <summary>
        /// Selects all Customers for the given Dog
        /// </summary>
        /// <param name="dogModel">DogModel to get the Customers for</param>
        /// <returns></returns>
        ///
        List<CustomerModel> GetAllCustomerForDog(DogModel dModel);

        /// <summary>
        /// Adds a given note as String to the given CustomerModel to the data store
        /// </summary>
        /// <param name="customerModel">CustomerModel to add the note to</param>
        /// <param name="note">The note to add</param>
        /// <returns>CustomerModel with added note</returns>
        NoteModel AddNoteToCustomer(CustomerModel cModel, string note);

        /// <summary>
        /// Selects a single CustomerModel from the data store after updated it
        /// </summary>
        /// <param name="customerModel">CustomerModel</param>
        /// <returns>CustomerModel with all values filled in</returns>
        CustomerModel Get_Customer(CustomerModel model);

        /// <summary>
        /// Selects all Customers from the data store 
        /// </summary>
        /// <returns>List of all Customers in the database active an inactiv </returns>
        List<CustomerModel> Get_CustomerInactiveAndActive();

        /// <summary>
        /// Selects all CustomerModels which matches with the searchtext 
        /// </summary>
        /// <param name="searchText">string what to search for</param>
        /// <param name="activeAndInactive">bool if searched for only active customers or also inactive</param>
        /// <returns>List of CustomerModel which matches with the searchtext</returns>
        List<CustomerModel> SearchResultsCustomer(string searchText, bool isActiveAndInactive);

        /// <summary>
        /// removes the give node from the customer
        /// </summary>
        /// <param name="noteModel">Note to delete from the List</param>
        /// <param name="customerModel">CustomerModel where the note to remove from</param>
        void DeleteNoteFromList(NoteModel noteModel, CustomerModel cModel);

        #endregion

        #region Dog

        /// <summary>
        /// Inserts the Dog into the Data store
        /// Inserts the DogToCustomer Relation into the Datastore
        /// Add the Customer to the DogModel Customer List
        /// </summary>
        /// <returns>
        /// Dog complete with id 
        /// </returns>
        DogModel AddDog(DogModel dModel, CustomerModel cModel);

        /// <summary>
        /// Updates a Dog in the Data store with new Attributes
        /// </summary>
        void UpdateDog(DogModel dModel);

        /// <summary>
        /// Inserts the Dog with Characteristics and Diseases in the Data store
        /// </summary>
        /// <param name="dModel">Dogmodel to save</param>
        /// <returns>Dogmodel with the ID from the Data store</returns>
        DogModel AddDogToDatabase(DogModel dModel);

        /// <summary>
        /// Inserts a DogToCustomer Relation in the Data store.
        /// </summary>
        /// <param name="dModel">DogModel ID which is needed to save the relation in Data store/param>
        /// <param name="cModel">CustomerModel ID which is needed to save the relation in the Data store</param>
        void AddDogToCustomer(DogModel dModel, CustomerModel cModel);

        /// <summary>
        /// Selects all active Dogs in the Data store
        /// </summary>
        /// <returns>List of DogModels</returns>
        List<DogModel> Get_DogsAll();

        /// <summary>
        /// Deletes the DogToCustomer relation entry in the Data store
        /// </summary>
        /// <param name="cModel">CutomerModel needed for the deletion</param>
        /// <param name="dModel">DogModel needed for the deletion</param>
        void DeleteDogToCustomerRelation(CustomerModel cModel, DogModel dModel);

        /// <summary>
        /// Deletes the Relation Between Dog and Disease in the Data store
        /// </summary>
        /// <param name="dModel">DogModel that holds a List of its own disease</param>
        void DeleteDogDiseasesRelation(DogModel dModel);

        /// <summary>
        /// Deletes the Relation Between Dog and Characteristics in the Data store
        /// </summary>
        /// <param name="dModel">Dogmodel that holds a List of its own characterisitcs</param>
        void DeleteDogToCharacteristicsRelation(DogModel dModel);

        /// <summary>
        /// Deletes the Diseases from the data store
        /// </summary>
        /// <param name="diseasesModel">Disease Model to delete from data store</param>
        void DeleteDiseases(DiseasesModel model);

        /// <summary>
        /// Deletes the given Characteristic from the data store
        /// </summary>
        /// <param name="characteristicsModel">Characteristics Model to delete from data store</param>
        void DeleteCharacteristics(CharacteristicsModel model);

        /// <summary>
        /// Delete the given Dog from the Data store
        /// </summary>
        /// <param name="dogModel">DogModel to delete from data store</param>
        void DeleteDogFromDatabase(DogModel model);

        /// <summary>
        /// Selects all inactive and active dogs from the data store and return the result as a List
        /// </summary>
        /// <returns>List<DogModel></returns>
        List<DogModel> Get_DogsInactiveAndActive();

        /// <summary>
        /// Searches in the Table Dogs for the given Text in every column
        /// </summary>
        /// <param name="searchText">string of the searchtext that was entered in the Textbox</param>
        /// <param name="activeAndInactive">bool if searched only for active dogs or active and inactive dogs</param>
        /// <returns>Returns a List of Dogmodel that  matches the searchtext</returns>
        List<DogModel> SearchResultDogs(string searchText, bool activeAndInactive);

        /// <summary>
        /// Selects a DogModel from the Data store
        /// </summary>
        /// <param name="id">DogModel id from the entry in the data store</param>
        /// <returns>a complete Dogmodel out of the data store</returns>
        DogModel GetDog(int id);

        #endregion

        #region User

        /// <summary>
        /// checks data store for user password combination
        /// </summary>
        /// <param name="userModel">UserModel that holds the username and password</param>
        /// <returns>true if the combination is right - false if the combination is wrong</returns>
        UserModel IsUserAndPasswordRight(UserModel input);

        /// <summary>
        /// Inserts a user into data store
        /// </summary>
        /// <param name="userModel"></param>
        void InsertUserToDatabase(UserModel model);

        /// <summary>
        /// Selects all active user from the data store
        /// </summary>
        /// <returns>Returns a List of UserModel</returns>
        List<UserModel> GetAllActiveUser();

        /// <summary>
        /// Deletes the given user from the data store
        /// </summary>
        /// <param name="userModel"></param>
        void DeleteUserFromDataBase(UserModel model);

        /// <summary>
        /// Selects all Users from Data store active also as inactive
        /// </summary>
        /// <returns>List of Users</returns>
        List<UserModel> GetAllUser();

        /// <summary>
        /// Updates the values of the given user in the data store
        /// </summary>
        /// <param name="userModel">User to update</param>
        void UpdateUser(UserModel model);

        /// <summary>
        /// Searches in the data store for the given Text
        /// </summary>
        /// <param name="searchText">TextBox content</param>
        /// <param name="showInactive">bool if searched only for active users or active and inactive users </param>
        /// <returns> List of UserModels</returns>
        List<UserModel> SearchResultUser(string searchText, bool showInactive);

        #endregion

        #region Product

        /// <summary>
        /// Selects all Products from the data store and writes it into a List of ProductModel 
        /// </summary>
        /// <returns>List of ProductModel</returns>
        List<ProductModel> GetAllProducts();

        /// <summary>
        /// Adds a product into the table product from the data store
        /// </summary>
        /// <param name="productModel">ProductModel to add</param>
        /// <returns>given ProductModel with id from data store table</returns>
        ProductModel AddProductToDataStore(ProductModel productModel);

        /// <summary>
        /// Deletes the product from the table product in data store
        /// </summary>
        /// <param name="productModel">ProductModel to delete</param>
        void DeleteProductFromDataStore(ProductModel productModel);

        /// <summary>
        /// Updates the values from the ProductModel in the data store with the values of the given ProductModel
        /// </summary>
        /// <param name="productModel">Product to update</param>
        void UpdateProduct(ProductModel productModel);

        /// <summary>
        /// Returns a List with ProductModel that matches the searchstring from the textbox and all or only the active products
        /// </summary>
        /// <param name="searchText">string from the TextBox </param>
        /// <param name="showInactive">bool only active or also inactive products</param>
        /// <returns>List of products that matches the search string and the bool</returns>
        List<ProductModel> SearchResultProducts(string searchText, bool showInactive);

        #endregion

        #region Appointment

        /// <summary>
        /// Selects all appointments for the given dog
        /// </summary>
        /// <param name="dogModel">DogModel to get the appointments for</param>
        /// <returns>List of AppointmentModels</returns>
        List<AppointmentModel> GetAppointmentsForDog(DogModel dogModel);

        /// <summary>
        /// Adds a Appointment for the dog to the data store
        /// </summary>
        /// <param name="appointmentModel">AppointmentModel to add</param>
        /// <returns>AppointmentModel with id</returns>
        AppointmentModel AddAppointmentToDataStore(AppointmentModel apointmentModel);

        /// <summary>
        /// Checks if the given AppointmentModel is already in the data store
        /// </summary>
        /// <param name="appointmentModel">AppointmentModel to search for</param>
        /// <returns>true if the given AppointmentModel is in data store false if not</returns>
        bool IsAppointmentInDataStore(AppointmentModel appointmentModel);

        /// <summary>
        /// Checks whether the dog is in the given TimeSpan already booked
        /// </summary>
        /// <param name="appointmentModel">AppointmentModel with the TimeSpan</param>
        /// <returns>Returns true if the dog is already booked in the given timespan</returns>
        bool IsDogInTimeSpanAlreadyInDataStore(AppointmentModel appointmentModel);

        /// <summary>
        /// Selects all ApointmentModel from the data store
        /// </summary>
        /// <returns>List of all Appointments from the data store</returns>
        List<AppointmentModel> GetAppointments();

        /// <summary>
        /// Updates the given Model in the Data store
        /// </summary>
        /// <param name="appointmentModel"></param>
        void EditAppointmentModel(AppointmentModel appointmentModel);

        /// <summary>
        /// Deletes the given Appointment Object from Data store
        /// </summary>
        /// <param name="appointmentModel"></param>
        void DeleteAppointmentModel(AppointmentModel appointmentModel);

        #endregion

        #region Invoices
        /// <summary>
        /// gets All Invoices from the Datastore
        /// </summary>
        /// <returns>List of Invoice Models</returns>
        List<InvoiceModel> Get_InvoicesActiveAndInactive();

        #endregion

        #region CityToZipcode

        /// <summary>
        /// Selects the Citys which matches with the given zipcode
        /// </summary>
        /// <param name="zipCode">String of german zipcode to search the citys for</param>
        /// <returns>List of Citys as a string value</returns>
        List<string> GetCityToZipcode(string zipCode);

        #endregion
    }
}
