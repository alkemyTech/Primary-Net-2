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
            BEGIN
                -- Verificar si existen Account_Id y To_Account_Id
                IF NOT EXISTS (SELECT * FROM Accounts WHERE Id = @Account_Id)
                BEGIN
                    RAISERROR('The issuing account does not exist', 16, 1);
                    RETURN;
                END
    
                IF @To_Account_Id IS NOT NULL AND NOT EXISTS (SELECT * FROM Accounts WHERE Id = @To_Account_Id)
                BEGIN
                    RAISERROR('La cuenta To_Account_Id no existe', 16, 1);
                    RETURN;
                END
    
                -- Insertar la nueva transacción
                INSERT INTO Transactions (Amount, Concept, Date, Type, Account_Id, To_Account_Id)
                VALUES (@Amount, @Concept, @Date, @Type, @Account_Id, @To_Account_Id);
            END            
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
