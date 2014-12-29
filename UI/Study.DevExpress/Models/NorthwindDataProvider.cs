using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Study.DevExpress.Models {
	// DXCOMMENT: Configure a data model (In this sample, we do this in file NorthwindDataProvider.cs. You would better create your custom file for a data model.)
    public static class NorthwindDataProvider {
        const string NorthwindDataContextKey = "DXNorthwindDataContext";

        public static NorthwindDataContext DB {
            get {
                if (HttpContext.Current.Items[NorthwindDataContextKey] == null)
                    HttpContext.Current.Items[NorthwindDataContextKey] = new NorthwindDataContext();
                return (NorthwindDataContext)HttpContext.Current.Items[NorthwindDataContextKey];
            }
        }

        public static IEnumerable GetCustomers() {
            return from customer in DB.Customers select customer;
        }
    }
}