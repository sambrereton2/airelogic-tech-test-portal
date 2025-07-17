# Aire Logic Tech Test Portal

Completed by Sam Brereton

## Overview

This repository started as a fork of the airelogic/tech-test-portal.

At the root of the repository is a Visual Studio solution file, TechTestPortal.sln which provides a solution for the Patient Appointment Backend task.

Solution Contents:

 - [PatientAppointmentBackend.Service](/Patient-Appointment-Backend/src/PatientAppointmentBackend.Service) .NET 8 Web Api Service
 - [PatientAppointmentBackend.Shared](/Patient-Appointment-Backend/src/PatientAppointmentBackend.Shared) .NET 8 class library 
 - [PatientAppointmentBackend.Data](/Patient-Appointment-Backend/src/PatientAppointmentBackend.Data) .NET 8 class library with Entity Framework models, Database context and Migrations.
 - [PatientAppointmentBackend.Tests](/Patient-Appointment-Backend/test/PatientAppointmentBackend.Tests) Unit Tests

## Development Notes

 - So that the customer is not locked into any particular database, Entity Framework was chosen. The solution has been configured to use Sqlite database which is created from migrations when the service is first started, so it can run without any external dependencies.
 - All database related models, migrations etc are maintained in a sepearate class library.
 - To aid consuming the Web Api, Swagger is configured with Api and Schema documentation support. The Swagger UI provides working samples of Api payloads and can be used to demo all functionality. Data is persisted over service restarts.
 - Serilog file logging is configured.
 - Models consumed and returned from the Web Api are separate from the Entities used by the database. This gives flexibility enabling the database schema to not be dependent on the incoming data and helps prevent exposing the underlying database layout. The Api and the database can both independently evolve. 
 - Custom ValidationAttribute's have been applied to Api models to perform NhsNumber validation and partial Postcode validation - which are covered by unit tests.