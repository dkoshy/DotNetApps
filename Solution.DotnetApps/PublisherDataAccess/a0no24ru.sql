START TRANSACTION;

INSERT INTO "Authors" ("Id", "FirstName", "LastName")
VALUES (1, 'Frank', 'Herbert');
INSERT INTO "Authors" ("Id", "FirstName", "LastName")
VALUES (2, 'George', 'Orwell');
INSERT INTO "Authors" ("Id", "FirstName", "LastName")
VALUES (3, 'Julie', 'Lerman');
INSERT INTO "Authors" ("Id", "FirstName", "LastName")
VALUES (4, 'Julia', 'Lerman');
INSERT INTO "Authors" ("Id", "FirstName", "LastName")
VALUES (5, 'Deepak', 'Koshy');
INSERT INTO "Authors" ("Id", "FirstName", "LastName")
VALUES (6, 'Bini', 'Koshy');
INSERT INTO "Authors" ("Id", "FirstName", "LastName")
VALUES (7, 'Thomas', 'Koshy');

SELECT setval(
    pg_get_serial_sequence('"Authors"', 'Id'),
    GREATEST(
        (SELECT MAX("Id") FROM "Authors") + 1,
        nextval(pg_get_serial_sequence('"Authors"', 'Id'))),
    false);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260417075926_SeedAuthorDetails', '8.0.26');

COMMIT;

