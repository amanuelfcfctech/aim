CREATE PROCEDURE [dbo].[usp_Test_SEL_TestID]
	@TestID INT
AS
BEGIN
    SELECT
        T.TestID,
        T.TestCode,
        T.TestName,
        T.DataSourceTypeID,
		DS.DataSourceTypeCode AS DataSourceTypeName,
        T.TestCurrentCost,
	    T.TestPreviousCost,
        T.UpdateStamp,
        T.CFCUserID,
		U.UserName
    FROM dbo.Test T
	INNER JOIN
		dbo.DataSourceType DS
		ON T.DataSourceTypeID = DS.DataSourceTypeID
	INNER JOIN
		CFCuser.CFCUser U
		ON U.CFCUserID = T.CFCUserID
	WHERE
		T.TestID = @TestID
END;