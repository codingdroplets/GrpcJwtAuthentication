# gRPC JWT Authentication in .Net C#

Watch Tutorial Video: https://www.youtube.com/watch?v=4-GTX6vW2Z4

This is a complete course to develop .Net applications or services with gRPC. This video is part of a series and this is the fourth video of the gRPC C# Tutorial Series. In this video we are going to see about the gRPC JWT Token in .Net Core [gRPC JWT Token .Net Core and DotNet gRPC Authorization].

gRPC C# Tutorial Playlist link:
https://www.youtube.com/playlist?list=PLzewa6pjbr3IOa6POjAMM0xiPZ-shjoem

JSON Web Token (JWT) is an open standard (RFC 7519) that defines a compact and self-contained way for securely transmitting information between parties as a JSON object. This information can be verified and trusted because it is digitally signed. JWTs can be signed using a secret or a public/private key pair.

gRPC can be used with ASP.NET Core authentication to associate a user with each call [C# gRPC JWT Token].

The configuration of gRPC Service JWT Authentication is not different from a regular configuration that .NET Core API requires. Also, it doesn’t vary depending on the protocol which we use, HTTP or HTTPS. In a few words, you need to add standard authentication and authorization services and middleware in Program.cs file. The position of middleware is important.

Bearer Token Authentication:
The client can provide an access token for authentication. The server validates the token and uses it to identify the user. 

On the server, bearer token authentication is configured using the JWT Bearer middleware.
In the .NET gRPC client, the token can be sent with calls by using the Metadata collection. Entries in the Metadata collection are sent with a gRPC call as HTTP headers.

The class JwtTokenValidator is the one where you’ll define the validation logic. You will define TokenValidationParameters which will do all work for validation of JWT. And also, you can add an additional layer of security here. You may want to add it because JWT is a well-known format which means that if you have JWT you can go to https://jwt.io/ and see some information. I suggest to add additional encryption to JWT which makes it much harder to decrypt.
