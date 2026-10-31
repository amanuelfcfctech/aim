CREATE Procedure usp_LongProcess As Begin

    Declare @i Int;
    Declare @msg VarChar(50);
    Set @i = 0;
    while (@i < 100) Begin
        WaitFor Delay '00:00:01';

        Set @i = @i + 20;
        Set @msg = Convert(VarChar(10), @i) + ' PERCENT COMPLETE';		
        RaisError(@msg, 1, 1) With NoWait
		
    End
End