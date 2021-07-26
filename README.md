# net5-react
Base app for net5 and React.
Layers:
- API: Layer that let's external applications interact with this one.
- Core:  Layer that manages the business objects and their rules. Example: when the address is created it should also be saves in the google account
- EntityFramework: Layer that manages the connection to the database. It has all the configuration of the tables and migrations.

TODO:
- Logging
- Manager exceptions so they are showed in a user friendly manner
- Comments
- Modules
- Application layer that orchastrates bussiness objects to perfom task specific application tasks
- Users, authentication, authorization
- CORS
- Abstract the configuration settings to interface