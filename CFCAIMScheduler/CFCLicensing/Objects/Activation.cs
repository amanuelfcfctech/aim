using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using CFCLicensing.Entity;

namespace CFCLicensing.Object
{
	public static class Activation
	{
		#region Member Variables
		static long _configID = 0;
		static DateTime _installDate = DateTime.Now;
		static Boolean _isDaysLeftDisplayed = false;
		static Boolean _isLicenseAgreementAccepted = false;
		static Boolean _isMainFormLoaded = false;
		static Boolean _isProductLicense = false;
        //static Boolean _isProductLeased = false;
		static Boolean _isTrialExpired = false;
		static Boolean _isTrialExtended = false;
		static Boolean _isError = false;
		static Int64 _leaseLenght = 0;
		static string _licenseCompanyName = String.Empty;
		static CFCLicenseAgreement _licenseAgreement;
		static CFCProductType _productType;
        static Int64 _trialUseCount = 0;
        static Int32 _trialDaysLeft = 0;

		#endregion

		#region Properties
		public static long ConfigID
		{
			get
			{
				return _configID;
			}
			set
			{
				_configID = value;
			}
		}

		public static Boolean IsDaysLeftDisplayed
		{
			get
			{
				return _isDaysLeftDisplayed;
			}
			set
			{
				_isDaysLeftDisplayed = value;
			}
		}

		public static Boolean IsError
		{
			get
			{
				return _isError;
			}
			set
			{
				_isError = value;
			}
		}

		public static Int64 LeaseLenght
		{
			get
			{
				return _leaseLenght;
			}
			set
			{
				_leaseLenght = value;
			}
		}

		public static bool IsLicenseAccepted
		{
			get
			{
				return _isLicenseAgreementAccepted;
			}
			set
			{
				_isLicenseAgreementAccepted = value;
			}
		}

		public static bool IsMainFormLoaded
		{
			get
			{
				return _isMainFormLoaded;
			}
			set
			{
				_isMainFormLoaded = value;
			}
		}
        public static bool IsProductLicensed
        {
            get
            {
                return _isProductLicense;
            }
            set
            {
                _isProductLicense = value;
            }
        }

		public static bool IsTrialExtended
		{
			get
			{
				return _isTrialExtended;
			}
			set
			{
				_isTrialExtended = value;
			}
		}

		public static bool IsTrialExpired
		{
			get
			{
				return _isTrialExpired;
			}
			set
			{
				_isTrialExpired = value;
			}
		}

		public static CFCLicenseAgreement LicenseAgreement
		{
			get
			{
				return _licenseAgreement;
			}
			set
			{
				_licenseAgreement = value;
			}
		}
		public static string LicenseCompanyName
		{
			get
			{
				return _licenseCompanyName;
			}
			set
			{
				_licenseCompanyName = value;
			}
		}

		public static CFCProductType ProductType
		{
			get
			{
				return _productType;
			}
			set
			{
				_productType = value;
			}
		}

		public static DateTime InstallDate
		{
			get
			{
				return _installDate;
			}
			set
			{
				_installDate = value;
			}
		}

        public static Int64 TrialUseCount
        {
            get
            {
                return _trialUseCount;
            }
            set
            {
                _trialUseCount = value;
            }
        }

        public static Int32 TrialDaysLeft
        {
            get
            {
                return _trialDaysLeft;
            }
            set
            {
                _trialDaysLeft = value;
            }
        }
		#endregion

		#region
		public static decimal GetHex(string companyName)
		{
			decimal total = 100000;
			char[] values = companyName.ToCharArray();

			foreach (char letter in values)
			{
				// Get the integral value of the character.
				int value = Convert.ToInt32(letter);
				total += value;
			}
			return total;
		}

		public static decimal GetPCLicenseKey(string companyName)
		{
			//RegistryKey registry = null;
			//byte[] digitalProductId = null;
			decimal total = 100000;
			//TODO Get it to work with all windows license
			//registry = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", false);
			//digitalProductId = (byte[])registry.GetValue("DigitalProductId");

			//if (digitalProductId == null)
			//{
			total = GetHex(companyName);
			//}
			//else
			//{
			//    foreach (byte digital in digitalProductId)
			//    {
			//        // Get the integral value of the byte.
			//        int value = Convert.ToInt32(digital);
			//        total += value;
			//    }
			//}
			return total;
		}

		public static decimal GetInstallCode()
		{
			decimal installCode = 0;
			if (_licenseCompanyName == null || _licenseCompanyName == "")
			{
				_licenseCompanyName = Properties.Settings.Default.SoftwareCompanyName;
			}

			installCode += GetPCLicenseKey(_licenseCompanyName);
			installCode += Math.Round(Convert.ToDecimal(_installDate.ToOADate()), 0);
			installCode += Convert.ToDecimal(_productType) * 10000000;
			return installCode;
		}

		public static string GetProgramName()
		{
			switch (_productType)
			{
				case CFCProductType.Fertilizer:
					return "Fertilizer Blending System";
				case CFCProductType.Horse:
					return "Horse Blending System";
                case CFCProductType.PetFood:
                    return "Pet Food Blending System";
                case CFCProductType.CFCAIM:
                    return "CFC AIMS";
				default:
					return "CFC Tech System";
			}
		}

        public static Int64 GetTrialUseCount()
        {
            if (DateTime.Today.Subtract(_installDate).Days < 31)
            {
                _trialUseCount -= 1;
            }
            else
            {
                _trialUseCount = 0;
            }
            return _trialUseCount;
        }

        public static Int32 GetTrialDaysLeft()
        {
            TimeSpan daysLeft;
            int daysRemaining = 30;

            daysLeft = DateTime.Now.Subtract(Activation.InstallDate);

            if (daysLeft.Days > 0)
            {
                daysRemaining -= daysLeft.Days;
            }
            return daysRemaining;
        }

		public static Boolean GetValidateAuthorizationCode(decimal? fullauthorizationCode, Int64 leaseLenght)
		{
			Boolean authorization = false;
			decimal installCode = GetInstallCode();

			switch (_productType)
			{
				case CFCProductType.CFCAIM:
					switch (LicenseAgreement)
					{
						case CFCLicenseAgreement.Trail:
							if (fullauthorizationCode == ((installCode + 3333300) * 2 * 1000) + 060)
							{
								authorization = true;
							}
							break;
						case CFCLicenseAgreement.License:
							if (fullauthorizationCode == ((installCode + 2222200) * 2 * 1000) + 789)
							{
								authorization = true;
							}
							break;
						case CFCLicenseAgreement.Lease:
							if (fullauthorizationCode == ((installCode + 1234500) * 2 * 1000) + leaseLenght)
							{
								authorization = true;
							}
							break;
						default:
							break;
					}
					break;
                case CFCProductType.Fertilizer:
                    switch (LicenseAgreement)
                    {
                        case CFCLicenseAgreement.Trail:
                            if (fullauthorizationCode == ((installCode + 3333300) * 2 * 1000) + 060)
                            {
                                authorization = true;
                            }
                            break;
                        case CFCLicenseAgreement.License:
                            if (fullauthorizationCode == ((installCode + 2222200) * 2 * 1000) + 789)
                            {
                                authorization = true;
                            }
                            break;
                        case CFCLicenseAgreement.Lease:
                            if (fullauthorizationCode == ((installCode + 1234500) * 2 * 1000) + leaseLenght)
                            {
                                authorization = true;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
				case CFCProductType.Horse:
					break;
				case CFCProductType.PetFood:
					break;
			}
			return authorization;
		}

		public static void SetActivationValues(ref ConfigInfo config)
		{
			Activation.ConfigID = config.ConfigID;
			Activation.LicenseCompanyName = config.CompanyName;
			Activation.InstallDate = config.InstallDate;
			Activation.TrialUseCount = config.TrialUseCount;
		}
		#endregion
	}
}
