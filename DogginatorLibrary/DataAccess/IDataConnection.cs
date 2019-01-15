using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogginatorLibrary.DataAccess
{
    public interface IDataConnection
    {
       /// <summary>
       /// This Method should connect to the Database and get Information about all saved Customers
       /// </summary>
       /// <returns>
       /// Returns a List of CustomerModels
       /// </returns>
        List<CustomerModel> Get_CustomerAll();

        /// <summary>
        /// This Method should connect to the Database and get Information about all saved Dogs
        /// </summary>
        /// <returns>
        /// Returns a List of DogModels
        /// </returns>
        List<DogModel> Get_DogsAll();

        /// <summary>
        /// Adds the Customer Model
        /// </summary>
        /// <returns>
        /// Customer complete with id 
        /// </returns>
        CustomerModel AddCustomer(CustomerModel cModel);

        /// <summary>
        /// Deletes a Customer Model
        /// </summary>
        /// <param name="model"></param>
        void DeleteCustomer(CustomerModel model);

        /// <summary>
        /// Updates a Customer
        /// </summary>
        void UpdateCustomer(CustomerModel cModel);

        /// <summary>
        /// Adds the Dog Model
        /// </summary>
        /// <returns>
        /// Dog complete with id 
        /// </returns>
        DogModel AddDog(DogModel dModel, CustomerModel cModel);

        /// <summary>
        /// Updates a Dog
        /// </summary>
        void UpdateDog(DogModel dModel);

        /// <summary>
        /// Saves the new Note about the Customer in the DataBase
        /// </summary>
        /// <param name="cModel">Customermodel with the correct ID</param>
        /// <param name="note">Note to save</param>
        /// <returns>The Notemodel with the ID</returns>
        NoteModel AddNoteToCustomer(CustomerModel cModel, string note);

        CustomerModel Get_Customer(CustomerModel model);

        void DeleteNoteFromList(NoteModel noteModel, CustomerModel cModel);

        DogModel AddDogToDatabase(DogModel dModel);

        void AddDogToCustomer(DogModel dModel, CustomerModel cModel);

        List<CustomerModel> GetAllCustomerForDog(DogModel dModel);

        void DeleteDogToCustomerRelation(CustomerModel cModel, DogModel dModel);

    }
}
