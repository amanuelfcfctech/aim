namespace CFCLicensing.Object
{
	/// <summary>Enumeration for Activation function</summary>
	public enum CFCProductType
	{
		/// <summary>Fertilizer</summary>
		Fertilizer = 1,

		/// <summary>Horse</summary>
		Horse = 2,

		/// <summary>PetFood</summary>
		PetFood = 3,

        /// <summary>CFCAIM</summary>
        CFCAIM = 4
	}

	/// <summary>Enumeration for Activation Code</summary>
	public enum CFCAuthorizationCode
	{
		/// <summary>Bad Activation Code</summary>
		InvalidCode = 0,

		/// <summary>Extend 30 Day Trial Version</summary>
		ExtendTrial = 1,

		/// <summary>Single User</summary>
		SingleUser = 2,

		/// <summary>University License</summary>
		University = 3,

		/// <summary>Lease License</summary>
		Lease = 4
	}

	/// <summary>Enumeration for License Agreement</summary>
	public enum CFCLicenseAgreement
	{
		/// <summary>Trail Agreement</summary>
		Trail = 0,

		/// <summary>License Agreement</summary>
		License = 1,

		/// <summary>Lease Agreement</summary>
		Lease = 2
	}
}
