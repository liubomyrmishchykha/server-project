using System;
using Models;

namespace WcfServiceLibrary.DataModel
{
    public class Convertor
    {
        /// <summary>
        /// Method wich convert type SearchDataIn to SerachDataOut
        /// </summary>
        /// <param name="search"></param>
        /// <returns>SearchDataOut</returns>
        public SearchDataOut Parser(SearchDataIn search)
        {
            try
            {
                SearchDataOut sdo = new SearchDataOut();
                sdo.ItemsPerPage = search.ItemsPerPage;
                sdo.CurrentPage = search.CurrentPage;
                sdo.OrderbyField = search.OrderbyField;
                sdo.OrderBy = search.OrderByReverse;
                sdo.InstanceName = string.IsNullOrEmpty(search.FilterByFields.InstanceName) ? null : search.FilterByFields.InstanceName;
                sdo.HostName = string.IsNullOrEmpty(search.FilterByFields.HostName) ? null : search.FilterByFields.HostName;
                sdo.Id = ConvertToNulable.ToNullableInt(search.FilterByFields.Id);
                sdo.AddedFrom = ConvertToNulable.ToNullableDateTime(search.FilterByFields.AddedFrom);
                sdo.AddedTo = ConvertToNulable.ToNullableDateTime(search.FilterByFields.AddedTo);
                sdo.ModifiedFrom = ConvertToNulable.ToNullableDateTime(search.FilterByFields.ModifiedFrom);
                sdo.ModifiedTo = ConvertToNulable.ToNullableDateTime(search.FilterByFields.ModifiedTo);
                sdo.Status = ConvertToNulable.ToNullableInt(search.FilterByFields.Status);
                sdo.RAM = ConvertToNulable.ToNullableInt(search.FilterByFields.RAM);
                sdo.CPUCount = ConvertToNulable.ToNullableInt(search.FilterByFields.CPUCount);
                sdo.UserName = string.IsNullOrWhiteSpace(search.FilterByFields.UserName) ? null : search.FilterByFields.UserName;
                sdo.Version = string.IsNullOrEmpty(search.FilterByFields.Version) ? null : search.FilterByFields.Version;

                return sdo;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}
