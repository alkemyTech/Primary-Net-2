using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimatesWallet.Infrastructure.Migrations
{
    public partial class StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE InsertTransactionWithValidation
                @Amount decimal(18,2),
                @Concept VARCHAR(50),
                @Date DATETIME,
                @Type VARCHAR(15),
                @Account_Id INT,
                @To_Account_Id INT
            AS
            BEGIN TRY
                -- Verificar si existen Account_Id y To_Account_Id
                IF NOT EXISTS (SELECT * FROM Accounts WHERE Id = @Account_Id)
                BEGIN
                    RAISERROR('The sending account does not exist.', 16, 1);
                    RETURN;
                END
    
                IF NOT EXISTS (SELECT * FROM Accounts WHERE Id = @To_Account_Id)
                BEGIN
                    RAISERROR('The destination account is invalid.', 16, 1);
                    RETURN;
                END

                BEGIN TRANSACTION;
                    -- Bloque if else para operaciones en cuenta segun tipo de transaction
                    IF @Type = 'topup'
                    BEGIN
                        -- Agregamos el dinero a la cuenta
                        UPDATE Accounts SET Money = Money + @Amount WHERE Id = @Account_Id;
                    END

                    ELSE
                    BEGIN
                        
                        --Verificamos que la cuenta tenga fondos suficientes

                        DECLARE @balance DECIMAL(18,0);
                        SELECT @balance = Money FROM Accounts WHERE Id = @Account_Id;
                        IF (@Amount > @balance)
                        BEGIN
                            RAISERROR('The account balance is insufficient to complete the transaction.', 16, 1);
                            RETURN;
                        END

                        -- Realizamos las operaciones entre las cuentas
                        UPDATE Accounts SET Money = Money - @Amount WHERE Id = @Account_Id;
                        UPDATE Accounts SET Money = Money + @Amount WHERE Id = @To_Account_Id;

                    END
    
                    -- Insertar la nueva transacción
                    INSERT INTO Transactions (Amount, Concept, Date, Type, Account_Id, To_Account_Id)
                    VALUES (@Amount, @Concept, @Date, @Type, @Account_Id, @To_Account_Id);

                COMMIT TRANSACTION;

            END TRY
            BEGIN CATCH
                IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

				--Atrapamos el error del try
				DECLARE @ErrorMessage NVARCHAR(4000);
				DECLARE @ErrorSeverity INT;
				DECLARE @ErrorState INT;

				SELECT 
					@ErrorMessage = ERROR_MESSAGE(),
					@ErrorSeverity = ERROR_SEVERITY(),
					@ErrorState = ERROR_STATE();

				RAISERROR (@ErrorMessage, @ErrorSeverity,@ErrorState );
            END CATCH
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
