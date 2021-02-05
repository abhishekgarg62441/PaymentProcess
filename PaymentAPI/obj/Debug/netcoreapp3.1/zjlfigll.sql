IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [ProcessPaymentDetail] (
    [PaymentID] int NOT NULL IDENTITY,
    [CreditCardNumber] nvarchar(max) NULL,
    [CardHolder] nvarchar(max) NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [SecurityCode] nvarchar(max) NULL,
    [Amount] float NOT NULL,
    CONSTRAINT [PK_ProcessPaymentDetail] PRIMARY KEY ([PaymentID])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210204102633_PaymentAPI.Context.ProcessPaymentsContext', N'3.1.0');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210204102916_PaymentAPI.Context.ProcessPaymentsContextSeed', N'3.1.0');

GO

