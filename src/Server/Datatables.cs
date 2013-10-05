﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TaskLeader.Server
{
    [DataContract]
    public class DTanswer {

        #region Reference : http://datatables.net/usage/server-side
        //int	    iTotalRecords	        Total records, before filtering (i.e. the total number of records in the database)
        //int	    iTotalDisplayRecords	Total records, after filtering (i.e. the total number of records after filtering has been applied
        //                                  - not just the number of records being returned in this result set)
        //string	sEcho	                An unaltered copy of sEcho sent from the client side.
        //                                  This parameter will change with each draw (it is basically a draw count)
        //                                  - so it is important that this is implemented.
        //                                  Note that it strongly recommended for security reasons that you 'cast' this parameter to an integer
        //                                  in order to prevent Cross Site Scripting (XSS) attacks.
        //string	sColumns	            Deprecated Optional - this is a string of column names, comma separated (used in combination with sName) which will allow DataTables to reorder data on the client-side if required for display. Note that the number of column names returned must exactly match the number of columns in the table. For a more flexible JSON format, please consider using mData.
        //                                  Note that this parameter is deprecated and will be removed in v1.10. Please now use mData.
        //array	    aaData                  The data in a 2D array. Note that you can change the name of this parameter with sAjaxDataProp.
        #endregion

        public DTanswer() {
        }

        [DataMember]
        public int sEcho { get; set; }
        [DataMember]
        public int iTotalRecords { get; set; }
        [DataMember]
        public int iTotalDisplayRecords { get; set; }
        [DataMember]
        public List<List<string>> aaData { get; set; }
        [DataMember]
        public string sColumns { get; set; } //DEPRECATED

        public void Import(string[] properties)
        {
            sColumns = string.Empty;
            for (int i = 0; i < properties.Length; i++)
            {
                sColumns += properties[i];
                if (i < properties.Length - 1)
                    sColumns += ",";
            }
        }
    }

    public class DTrequest {

        #region Reference : http://datatables.net/usage/server-side
        //int       iDisplayStart	    Display start point in the current data set.
        //int	    iDisplayLength	    Number of records that the table can display in the current draw.
        //                              It is expected that the number of records returned will be equal to this number,
        //                              unless the server has fewer records to return.
        //int	    iColumns	        Number of columns being displayed (useful for getting individual column search info)
        //string	sSearch	            Global search field
        //bool	    bRegex	            True if the global filter should be treated as a regular expression for advanced filtering, false if not.
        //bool	    bSearchable_(int)	Indicator for if a column is flagged as searchable or not on the client-side
        //string	sSearch_(int)	    Individual column filter
        //bool	    bRegex_(int)	    True if the individual column filter should be treated as a regular expression for advanced filtering,
        //                              false if not
        //bool	    bSortable_(int)	    Indicator for if a column is flagged as sortable or not on the client-side
        //int	    iSortingCols	    Number of columns to sort on
        //int	    iSortCol_(int)	    Column being sorted on (you will need to decode this number for your database)
        //string	sSortDir_(int)	    Direction to be sorted - "desc" or "asc".
        //string	mDataProp_(int)	    The value specified by mDataProp for each column.
        //                              This can be useful for ensuring that the processing of data is independent from the order of the columns.
        //string	sEcho	            Information for DataTables to use for rendering.
        #endregion

        private HashSet<String> DTrequestParams;

        private NameValueCollection param;

        public DTrequest(NameValueCollection parameters) {
            DTrequestParams = new HashSet<String>(new String[] {
                "iDisplayStart",
                "iDisplayLength",
                "iColumns",
                "sSearch",
                "bRegex",
                "bSearchable_(int)",
                "sSearch_(int)",
                "bRegex_(int)",
                "bSortable_(int)",
                "iSortingCols",
                "iSortCol_(int)",
                "sSortDir_(int)",
                "mDataProp_(int)",
                "sEcho"
            }, StringComparer.InvariantCultureIgnoreCase); //TODO: trouver un moyen de générer les "_(int)"

            this.param = parameters;
        }

        public bool paramsAreValid(){
            return DTrequestParams.IsSupersetOf(this.param.AllKeys);
        }

        public DTanswer getData(){
            return new DTanswer();
        }
    }
}
