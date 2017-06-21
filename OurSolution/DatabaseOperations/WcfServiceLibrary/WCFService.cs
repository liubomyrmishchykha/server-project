using System;
using System.Collections.Generic;
using Repository;
using Models;
using WcfServiceLibrary.DataModel;
using WcfServiceLibrary.ExceptionHandler;
using System.Linq;

namespace WcfServiceLibrary
{
    [GlobalExceptionBehavior(typeof(GlobalExceptionHandler))]
    public class WCFService : IWcfService
    {
        private RepositoryFactory globalRepository = RepositoryFactory.Instance;

        //Repository<Instance> repoInstans = new Repository<Instance>();
        //Repository<User> repoUser = new Repository<User>();
        //Repository<InstanceWithUserInfo> repoInstanceWithUserInfo  = new Repository<InstanceWithUserInfo>();
        //Repository<Options> repoOption  = new Repository<Options>();


        /// <summary>
        /// Method which Add new user in DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Condition type</returns>
        public void AddUser(User user)
        {
            globalRepository.repoUser.Add(user);
            //repoUser.Add(user);
        }

        /// <summary>
        /// Method which get specific Instance by Id from DB
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Instance type</returns>
        public InstanceWithUserInfo GetInstansById(string Id)
        {
            return globalRepository.repoInstanceWithUserInfo.SelectInstanceByID(int.Parse(Id));
        }

        /// <summary>
        /// Method which Get all Instances from DB
        /// </summary>
        /// <returns>List<Instance></returns>
        public List<Instance> GetInstanceList()
        {
            return globalRepository.repoInstans.GetAll();
        }

        /// <summary>
        /// Method which Search Instances by SearchDataIn in DB
        /// </summary>
        /// <param name = "search" ></ param >
        /// < returns > InstanceWithUserInfo type</returns>
        public SearchResult GetInstansByData(SearchDataIn search)
        {
            Convertor con = new Convertor();
            SearchDataOut sdo = con.Parser(search);
            var temp = globalRepository.repoInstanceWithUserInfo.Search(sdo);
            int count = temp.Item1;
            var instances = temp.Item2;
            SearchResult result = new SearchResult(instances, count);
            return result;
        }

        /// <summary>
        /// Method which Update user credentials in DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Condition type</returns>
        public void UpdateUser(User user)
        {
            globalRepository.repoUser.Update(user);
        }

        /// <summary>
        ///  Method which delete user by id from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Condition type</returns>
        public void DeleteUser(string id)
        {
            int number;
            if(!int.TryParse(id, out number))
                throw new ArgumentException("Passed value is not a  number");
            globalRepository.repoUser.Delete(number);
        }

        /// <summary>
        /// Method which Get all Users from DB
        /// </summary>
        /// <returns>List<User></returns>
        public List<User> GetAllUsers()
        {
            List<User> users = globalRepository.repoUser.GetAll();
            users.Select(s =>  s.Password="" ).ToList();
            return users;
        }

        /// <summary>
        /// Method which gets specific User by Id from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User type</returns>
        public User GetUserById(string id)
        {
            int number;
            if (!int.TryParse(id, out number))
                throw new ArgumentException("Passed value is not a  number");
            return globalRepository.repoUser.SelectByID(number);
        }

        /// <summary>
        /// Method which Update Options value in DB
        /// </summary>
        /// <param name="options"></param>
        public void UpdateOptions(Options options)
        {
            globalRepository.repoOption.Update(options);
        }

        /// <summary>
        /// Method which get specific Option by Id from DB
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Options GetOption()
        {
            return globalRepository.repoOption.SelectByID(1);
        }
    }
}