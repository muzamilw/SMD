﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Mapping.EntityViewGenerationAttribute(typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets664428598FEB4D2674C94A3B39B115436B075B1D2AAD2949465F376CA09523C1))]

namespace Edm_EntityMappingGeneratedViews
{
    
    
    /// <Summary>
    /// The type contains views for EntitySets and AssociationSets that were generated at design time.
    /// </Summary>
    public sealed class ViewsForBaseEntitySets664428598FEB4D2674C94A3B39B115436B075B1D2AAD2949465F376CA09523C1 : System.Data.Mapping.EntityViewContainer
    {
        
        /// <Summary>
        /// The constructor stores the views for the extents and also the hash values generated based on the metadata and mapping closure and views.
        /// </Summary>
        public ViewsForBaseEntitySets664428598FEB4D2674C94A3B39B115436B075B1D2AAD2949465F376CA09523C1()
        {
            this.EdmEntityContainerName = "BaseDbContext";
            this.StoreEntityContainerName = "SMDModelStoreContainer";
            this.HashOverMappingClosure = "c3dbe04920176f64fa89c2c0996a88a38897cdd640a45610c8934d2e6421b50b";
            this.HashOverAllExtentViews = "38e058fbad73fb00ff8421f1a63fb765786aef1985c938ad7de3c4685b1303f1";
            this.ViewCount = 8;
        }
        
        /// <Summary>
        /// The method returns the view for the index given.
        /// </Summary>
        protected override System.Collections.Generic.KeyValuePair<string, string> GetViewAt(int index)
        {
            if ((index == 0))
            {
                return GetView0();
            }
            if ((index == 1))
            {
                return GetView1();
            }
            if ((index == 2))
            {
                return GetView2();
            }
            if ((index == 3))
            {
                return GetView3();
            }
            if ((index == 4))
            {
                return GetView4();
            }
            if ((index == 5))
            {
                return GetView5();
            }
            if ((index == 6))
            {
                return GetView6();
            }
            if ((index == 7))
            {
                return GetView7();
            }
            throw new System.IndexOutOfRangeException();
        }
        
        /// <Summary>
        /// return view for SMDModelStoreContainer.AspNetRoles
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView0()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("SMDModelStoreContainer.AspNetRoles", @"
    SELECT VALUE -- Constructing AspNetRoles
        [SMDModel.Store.AspNetRoles](T1.AspNetRoles_Id, T1.AspNetRoles_Name)
    FROM (
        SELECT 
            T.Id AS AspNetRoles_Id, 
            T.Name AS AspNetRoles_Name, 
            True AS _from0
        FROM BaseDbContext.Roles AS T
    ) AS T1");
        }
        
        /// <Summary>
        /// return view for SMDModelStoreContainer.AspNetUserLogins
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView1()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("SMDModelStoreContainer.AspNetUserLogins", @"
    SELECT VALUE -- Constructing AspNetUserLogins
        [SMDModel.Store.AspNetUserLogins](T1.AspNetUserLogins_LoginProvider, T1.AspNetUserLogins_ProviderKey, T1.AspNetUserLogins_UserId)
    FROM (
        SELECT 
            T.LoginProvider AS AspNetUserLogins_LoginProvider, 
            T.ProviderKey AS AspNetUserLogins_ProviderKey, 
            T.UserId AS AspNetUserLogins_UserId, 
            True AS _from0
        FROM BaseDbContext.UserLogins AS T
    ) AS T1");
        }
        
        /// <Summary>
        /// return view for SMDModelStoreContainer.AspNetUsers
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView2()
        {
            System.Text.StringBuilder viewString = new System.Text.StringBuilder(4297);
            viewString.Append("\r\n    SELECT VALUE -- Constructing AspNetUsers\r\n        [SMDModel.Store.AspNetUs");
            viewString.Append("ers](T1.AspNetUsers_Id, T1.AspNetUsers_Email, T1.AspNetUsers_EmailConfirmed, T1.");
            viewString.Append("AspNetUsers_PasswordHash, T1.AspNetUsers_SecurityStamp, T1.AspNetUsers_PhoneNumb");
            viewString.Append("er, T1.AspNetUsers_PhoneNumberConfirmed, T1.AspNetUsers_TwoFactorEnabled, T1.Asp");
            viewString.Append("NetUsers_LockoutEndDateUtc, T1.AspNetUsers_LockoutEnabled, T1.AspNetUsers_Access");
            viewString.Append("FailedCount, T1.AspNetUsers_UserName, T1.AspNetUsers_UserDomainKey, T1.AspNetUse");
            viewString.Append("rs_FullName, T1.AspNetUsers_AlternateEmail, T1.AspNetUsers_IsEmailVerified, T1.A");
            viewString.Append("spNetUsers_Status, T1.AspNetUsers_CreatedDateTime, T1.AspNetUsers_ModifiedDateTi");
            viewString.Append("me, T1.AspNetUsers_LastLoginTime, T1.AspNetUsers_Phone1, T1.AspNetUsers_Phone2, ");
            viewString.Append("T1.AspNetUsers_Jobtitle, T1.AspNetUsers_ContactNotes, T1.AspNetUsers_IsSubscribe");
            viewString.Append("d, T1.AspNetUsers_isDefault, T1.AspNetUsers_LoginToken, T1.AspNetUsers_EmailSign");
            viewString.Append("ature, T1.AspNetUsers_EstimateHeadNotes, T1.AspNetUsers_EstimateFootNotes, T1.As");
            viewString.Append("pNetUsers_AppID, T1.AspNetUsers_CompanyName, T1.AspNetUsers_SalesEmail, T1.AspNe");
            viewString.Append("tUsers_CompanyRepresentative, T1.AspNetUsers_Address1, T1.AspNetUsers_Address2, ");
            viewString.Append("T1.AspNetUsers_CityID, T1.AspNetUsers_CountryID, T1.AspNetUsers_State, T1.AspNet");
            viewString.Append("Users_ZipCode, T1.AspNetUsers_UserTimeZone, T1.AspNetUsers_ReferralCode, T1.AspN");
            viewString.Append("etUsers_AfilliatianStatus, T1.AspNetUsers_ChargeBeeCustomerID, T1.AspNetUsers_Ch");
            viewString.Append("argeBeesubscriptionID, T1.AspNetUsers_RegisteredViaReferral, T1.AspNetUsers_Refe");
            viewString.Append("rringUserID)\r\n    FROM (\r\n        SELECT \r\n            T.Id AS AspNetUsers_Id, \r");
            viewString.Append("\n            T.Email AS AspNetUsers_Email, \r\n            T.EmailConfirmed AS Asp");
            viewString.Append("NetUsers_EmailConfirmed, \r\n            T.PasswordHash AS AspNetUsers_PasswordHas");
            viewString.Append("h, \r\n            T.SecurityStamp AS AspNetUsers_SecurityStamp, \r\n            T.P");
            viewString.Append("honeNumber AS AspNetUsers_PhoneNumber, \r\n            T.PhoneNumberConfirmed AS A");
            viewString.Append("spNetUsers_PhoneNumberConfirmed, \r\n            T.TwoFactorEnabled AS AspNetUsers");
            viewString.Append("_TwoFactorEnabled, \r\n            T.LockoutEndDateUtc AS AspNetUsers_LockoutEndDa");
            viewString.Append("teUtc, \r\n            T.LockoutEnabled AS AspNetUsers_LockoutEnabled, \r\n         ");
            viewString.Append("   T.AccessFailedCount AS AspNetUsers_AccessFailedCount, \r\n            T.UserNam");
            viewString.Append("e AS AspNetUsers_UserName, \r\n            T.UserDomainKey AS AspNetUsers_UserDoma");
            viewString.Append("inKey, \r\n            T.FullName AS AspNetUsers_FullName, \r\n            T.Alterna");
            viewString.Append("teEmail AS AspNetUsers_AlternateEmail, \r\n            T.IsEmailVerified AS AspNet");
            viewString.Append("Users_IsEmailVerified, \r\n            T.Status AS AspNetUsers_Status, \r\n         ");
            viewString.Append("   T.CreatedDateTime AS AspNetUsers_CreatedDateTime, \r\n            T.ModifiedDat");
            viewString.Append("eTime AS AspNetUsers_ModifiedDateTime, \r\n            T.LastLoginTime AS AspNetUs");
            viewString.Append("ers_LastLoginTime, \r\n            T.Phone1 AS AspNetUsers_Phone1, \r\n            T");
            viewString.Append(".Phone2 AS AspNetUsers_Phone2, \r\n            T.Jobtitle AS AspNetUsers_Jobtitle,");
            viewString.Append(" \r\n            T.ContactNotes AS AspNetUsers_ContactNotes, \r\n            T.IsSub");
            viewString.Append("scribed AS AspNetUsers_IsSubscribed, \r\n            T.isDefault AS AspNetUsers_is");
            viewString.Append("Default, \r\n            T.LoginToken AS AspNetUsers_LoginToken, \r\n            T.E");
            viewString.Append("mailSignature AS AspNetUsers_EmailSignature, \r\n            T.EstimateHeadNotes A");
            viewString.Append("S AspNetUsers_EstimateHeadNotes, \r\n            T.EstimateFootNotes AS AspNetUser");
            viewString.Append("s_EstimateFootNotes, \r\n            T.AppId AS AspNetUsers_AppID, \r\n            T");
            viewString.Append(".CompanyName AS AspNetUsers_CompanyName, \r\n            T.SalesEmail AS AspNetUse");
            viewString.Append("rs_SalesEmail, \r\n            T.CompanyRepresentative AS AspNetUsers_CompanyRepre");
            viewString.Append("sentative, \r\n            T.Address1 AS AspNetUsers_Address1, \r\n            T.Add");
            viewString.Append("ress2 AS AspNetUsers_Address2, \r\n            T.CityId AS AspNetUsers_CityID, \r\n ");
            viewString.Append("           T.CountryId AS AspNetUsers_CountryID, \r\n            T.State AS AspNet");
            viewString.Append("Users_State, \r\n            T.ZipCode AS AspNetUsers_ZipCode, \r\n            T.Use");
            viewString.Append("rTimeZone AS AspNetUsers_UserTimeZone, \r\n            T.ReferralCode AS AspNetUse");
            viewString.Append("rs_ReferralCode, \r\n            T.AfilliatianStatus AS AspNetUsers_AfilliatianSta");
            viewString.Append("tus, \r\n            T.ChargeBeeCustomerId AS AspNetUsers_ChargeBeeCustomerID, \r\n ");
            viewString.Append("           T.ChargeBeesubscriptionId AS AspNetUsers_ChargeBeesubscriptionID, \r\n ");
            viewString.Append("           T.RegisteredViaReferral AS AspNetUsers_RegisteredViaReferral, \r\n     ");
            viewString.Append("       T.ReferringUserId AS AspNetUsers_ReferringUserID, \r\n            True AS _");
            viewString.Append("from0\r\n        FROM BaseDbContext.Users AS T\r\n    ) AS T1");
            return new System.Collections.Generic.KeyValuePair<string, string>("SMDModelStoreContainer.AspNetUsers", viewString.ToString());
        }
        
        /// <Summary>
        /// return view for SMDModelStoreContainer.AspNetUserRoles
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView3()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("SMDModelStoreContainer.AspNetUserRoles", @"
    SELECT VALUE -- Constructing AspNetUserRoles
        [SMDModel.Store.AspNetUserRoles](T1.AspNetUserRoles_UserId, T1.AspNetUserRoles_RoleId)
    FROM (
        SELECT 
            Key(T.AspNetUsers).Id AS AspNetUserRoles_UserId, 
            Key(T.AspNetRoles).Id AS AspNetUserRoles_RoleId, 
            True AS _from0
        FROM BaseDbContext.AspNetUserRoles AS T
    ) AS T1");
        }
        
        /// <Summary>
        /// return view for BaseDbContext.Roles
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView4()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("BaseDbContext.Roles", @"
    SELECT VALUE -- Constructing Roles
        [DomainModels.Role](T1.Role_Id, T1.Role_Name)
    FROM (
        SELECT 
            T.Id AS Role_Id, 
            T.Name AS Role_Name, 
            True AS _from0
        FROM SMDModelStoreContainer.AspNetRoles AS T
    ) AS T1");
        }
        
        /// <Summary>
        /// return view for BaseDbContext.UserLogins
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView5()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("BaseDbContext.UserLogins", @"
    SELECT VALUE -- Constructing UserLogins
        [DomainModels.UserLogin](T1.UserLogin_LoginProvider, T1.UserLogin_ProviderKey, T1.UserLogin_UserId)
    FROM (
        SELECT 
            T.LoginProvider AS UserLogin_LoginProvider, 
            T.ProviderKey AS UserLogin_ProviderKey, 
            T.UserId AS UserLogin_UserId, 
            True AS _from0
        FROM SMDModelStoreContainer.AspNetUserLogins AS T
    ) AS T1");
        }
        
        /// <Summary>
        /// return view for BaseDbContext.Users
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView6()
        {
            System.Text.StringBuilder viewString = new System.Text.StringBuilder(3639);
            viewString.Append("\r\n    SELECT VALUE -- Constructing Users\r\n        [DomainModels.User](T1.User_Id");
            viewString.Append(", T1.User_Email, T1.User_EmailConfirmed, T1.User_PasswordHash, T1.User_SecurityS");
            viewString.Append("tamp, T1.User_PhoneNumber, T1.User_PhoneNumberConfirmed, T1.User_TwoFactorEnable");
            viewString.Append("d, T1.User_LockoutEndDateUtc, T1.User_LockoutEnabled, T1.User_AccessFailedCount,");
            viewString.Append(" T1.User_UserName, T1.User_UserDomainKey, T1.User_FullName, T1.User_AlternateEma");
            viewString.Append("il, T1.User_IsEmailVerified, T1.User_Status, T1.User_CreatedDateTime, T1.User_Mo");
            viewString.Append("difiedDateTime, T1.User_LastLoginTime, T1.User_Phone1, T1.User_Phone2, T1.User_J");
            viewString.Append("obtitle, T1.User_ContactNotes, T1.User_IsSubscribed, T1.User_isDefault, T1.User_");
            viewString.Append("LoginToken, T1.User_EmailSignature, T1.User_EstimateHeadNotes, T1.User_EstimateF");
            viewString.Append("ootNotes, T1.User_AppId, T1.User_CompanyName, T1.User_SalesEmail, T1.User_Compan");
            viewString.Append("yRepresentative, T1.User_Address1, T1.User_Address2, T1.User_CityId, T1.User_Cou");
            viewString.Append("ntryId, T1.User_State, T1.User_ZipCode, T1.User_UserTimeZone, T1.User_ReferralCo");
            viewString.Append("de, T1.User_AfilliatianStatus, T1.User_ChargeBeeCustomerId, T1.User_ChargeBeesub");
            viewString.Append("scriptionId, T1.User_RegisteredViaReferral, T1.User_ReferringUserId)\r\n    FROM (");
            viewString.Append("\r\n        SELECT \r\n            T.Id AS User_Id, \r\n            T.Email AS User_Em");
            viewString.Append("ail, \r\n            T.EmailConfirmed AS User_EmailConfirmed, \r\n            T.Pass");
            viewString.Append("wordHash AS User_PasswordHash, \r\n            T.SecurityStamp AS User_SecuritySta");
            viewString.Append("mp, \r\n            T.PhoneNumber AS User_PhoneNumber, \r\n            T.PhoneNumber");
            viewString.Append("Confirmed AS User_PhoneNumberConfirmed, \r\n            T.TwoFactorEnabled AS User");
            viewString.Append("_TwoFactorEnabled, \r\n            T.LockoutEndDateUtc AS User_LockoutEndDateUtc, ");
            viewString.Append("\r\n            T.LockoutEnabled AS User_LockoutEnabled, \r\n            T.AccessFai");
            viewString.Append("ledCount AS User_AccessFailedCount, \r\n            T.UserName AS User_UserName, \r");
            viewString.Append("\n            T.UserDomainKey AS User_UserDomainKey, \r\n            T.FullName AS ");
            viewString.Append("User_FullName, \r\n            T.AlternateEmail AS User_AlternateEmail, \r\n        ");
            viewString.Append("    T.IsEmailVerified AS User_IsEmailVerified, \r\n            T.Status AS User_St");
            viewString.Append("atus, \r\n            T.CreatedDateTime AS User_CreatedDateTime, \r\n            T.M");
            viewString.Append("odifiedDateTime AS User_ModifiedDateTime, \r\n            T.LastLoginTime AS User_");
            viewString.Append("LastLoginTime, \r\n            T.Phone1 AS User_Phone1, \r\n            T.Phone2 AS ");
            viewString.Append("User_Phone2, \r\n            T.Jobtitle AS User_Jobtitle, \r\n            T.ContactN");
            viewString.Append("otes AS User_ContactNotes, \r\n            T.IsSubscribed AS User_IsSubscribed, \r\n");
            viewString.Append("            T.isDefault AS User_isDefault, \r\n            T.LoginToken AS User_Lo");
            viewString.Append("ginToken, \r\n            T.EmailSignature AS User_EmailSignature, \r\n            T");
            viewString.Append(".EstimateHeadNotes AS User_EstimateHeadNotes, \r\n            T.EstimateFootNotes ");
            viewString.Append("AS User_EstimateFootNotes, \r\n            T.AppID AS User_AppId, \r\n            T.");
            viewString.Append("CompanyName AS User_CompanyName, \r\n            T.SalesEmail AS User_SalesEmail, ");
            viewString.Append("\r\n            T.CompanyRepresentative AS User_CompanyRepresentative, \r\n         ");
            viewString.Append("   T.Address1 AS User_Address1, \r\n            T.Address2 AS User_Address2, \r\n   ");
            viewString.Append("         T.CityID AS User_CityId, \r\n            T.CountryID AS User_CountryId, \r");
            viewString.Append("\n            T.State AS User_State, \r\n            T.ZipCode AS User_ZipCode, \r\n ");
            viewString.Append("           T.UserTimeZone AS User_UserTimeZone, \r\n            T.ReferralCode AS ");
            viewString.Append("User_ReferralCode, \r\n            T.AfilliatianStatus AS User_AfilliatianStatus, ");
            viewString.Append("\r\n            T.ChargeBeeCustomerID AS User_ChargeBeeCustomerId, \r\n            T");
            viewString.Append(".ChargeBeesubscriptionID AS User_ChargeBeesubscriptionId, \r\n            T.Regist");
            viewString.Append("eredViaReferral AS User_RegisteredViaReferral, \r\n            T.ReferringUserID A");
            viewString.Append("S User_ReferringUserId, \r\n            True AS _from0\r\n        FROM SMDModelStore");
            viewString.Append("Container.AspNetUsers AS T\r\n    ) AS T1");
            return new System.Collections.Generic.KeyValuePair<string, string>("BaseDbContext.Users", viewString.ToString());
        }
        
        /// <Summary>
        /// return view for BaseDbContext.AspNetUserRoles
        /// </Summary>
        private System.Collections.Generic.KeyValuePair<string, string> GetView7()
        {
            return new System.Collections.Generic.KeyValuePair<string, string>("BaseDbContext.AspNetUserRoles", @"
    SELECT VALUE -- Constructing AspNetUserRoles
        [DomainModels.AspNetUserRoles](T3.AspNetUserRoles_AspNetRoles, T3.AspNetUserRoles_AspNetUsers)
    FROM (
        SELECT -- Constructing AspNetRoles
            CreateRef(BaseDbContext.Roles, row(T2.AspNetUserRoles_AspNetRoles_Id), [DomainModels.Role]) AS AspNetUserRoles_AspNetRoles, 
            T2.AspNetUserRoles_AspNetUsers
        FROM (
            SELECT -- Constructing AspNetUsers
                T1.AspNetUserRoles_AspNetRoles_Id, 
                CreateRef(BaseDbContext.Users, row(T1.AspNetUserRoles_AspNetUsers_Id), [DomainModels.User]) AS AspNetUserRoles_AspNetUsers
            FROM (
                SELECT 
                    T.RoleId AS AspNetUserRoles_AspNetRoles_Id, 
                    T.UserId AS AspNetUserRoles_AspNetUsers_Id, 
                    True AS _from0
                FROM SMDModelStoreContainer.AspNetUserRoles AS T
            ) AS T1
        ) AS T2
    ) AS T3");
        }
    }
}


