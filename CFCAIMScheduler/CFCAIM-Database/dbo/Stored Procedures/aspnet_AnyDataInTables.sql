CREATE PROCEDURE [dbo].[aspnet_AnyDataInTables]
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT so.name FROM dbo.sysobjects so WHERE (so.name = N'vw_aspnet_MembershipUsers') AND (so.type = 'V'))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 mem.UserId FROM dbo.aspnet_Membership mem))
        BEGIN
            SELECT N'aspnet_Membership'
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0  AND
        (EXISTS (SELECT so.name FROM dbo.sysobjects so WHERE (so.name = N'vw_aspnet_Roles') AND (so.type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 r.RoleId FROM dbo.aspnet_Roles r))
        BEGIN
            SELECT N'aspnet_Roles'
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0  AND
        (EXISTS (SELECT so.name FROM sysobjects so WHERE (so.name = N'vw_aspnet_Profiles') AND (so.type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 p.UserId FROM dbo.aspnet_Profile p))
        BEGIN
            SELECT N'aspnet_Profile'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 8) is set
    IF ((@TablesToCheck & 8) <> 0  AND
        (EXISTS (SELECT so.name FROM dbo.sysobjects so WHERE (so.name = N'vw_aspnet_WebPartState_User') AND (so.type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 p.UserId FROM dbo.aspnet_PersonalizationPerUser p))
        BEGIN
            SELECT N'aspnet_PersonalizationPerUser'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 16) is set
    IF ((@TablesToCheck & 16) <> 0  AND
        (EXISTS (SELECT so.name FROM dbo.sysobjects so WHERE (so.name = N'aspnet_WebEvent_LogEvent') AND (so.type = 'P'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 e.* FROM dbo.aspnet_WebEvent_Events e))
        BEGIN
            SELECT N'aspnet_WebEvent_Events'
            RETURN
        END
    END

    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Users))
        BEGIN
            SELECT N'aspnet_Users'
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 a.ApplicationId FROM dbo.aspnet_Applications a))
        BEGIN
            SELECT N'aspnet_Applications'
            RETURN
        END
    END
END