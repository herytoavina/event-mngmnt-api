# Event Management API


## Installation

1. Clone the Repository

```bash
git clone https://github.com/herytoavina/event-mngmnt-api.git
cd event-mngmnt-api
```

2. Configure Database : 
```Update connection string in appsettings.json```
3.  Apply Database Migrations
```bash
Update-Database ( Nuget Console)
or 
dotnet ef database update (.NET CLI)
```
4. Run the Application : 
```The API will start at https://localhost:5012```
## API documentation

```json
{
    "openapi": "3.0.1",
    "info": {
        "title": "EventManagementAPI | v1",
        "version": "1.0.0"
    },
    "servers": [
        {
            "url": "http://localhost:5012"
        }
    ],
    "paths": {
        "/api/Event": {
            "get": {
                "tags": [
                    "Event"
                ],
                "parameters": [
                    {
                        "name": "startDate",
                        "in": "query",
                        "schema": {
                            "type": "string",
                            "format": "date-time"
                        }
                    },
                    {
                        "name": "endDate",
                        "in": "query",
                        "schema": {
                            "type": "string",
                            "format": "date-time"
                        }
                    },
                    {
                        "name": "category",
                        "in": "query",
                        "schema": {
                            "type": "string"
                        }
                    },
                    {
                        "name": "status",
                        "in": "query",
                        "schema": {
                            "type": "string"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            },
            "post": {
                "tags": [
                    "Event"
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/CreateEventDto"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/CreateEventDto"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/CreateEventDto"
                            }
                        }
                    },
                    "required": true
                },
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            }
        },
        "/api/Event/{id}": {
            "get": {
                "tags": [
                    "Event"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int64"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            },
            "put": {
                "tags": [
                    "Event"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int64"
                        }
                    }
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/UpdateEventDto"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/UpdateEventDto"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/UpdateEventDto"
                            }
                        }
                    },
                    "required": true
                },
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            },
            "delete": {
                "tags": [
                    "Event"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int64"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            }
        },
        "/api/Event/{id}/status": {
            "patch": {
                "tags": [
                    "Event"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int64"
                        }
                    }
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/UpdateEventStatusDto"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/UpdateEventStatusDto"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/UpdateEventStatusDto"
                            }
                        }
                    },
                    "required": true
                },
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            }
        },
        "/api/Event/{id}/registrations": {
            "get": {
                "tags": [
                    "Event"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int64"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            }
        },
        "/api/Login": {
            "post": {
                "tags": [
                    "Login"
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/User"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/User"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/User"
                            }
                        }
                    },
                    "required": true
                },
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            }
        },
        "/api/Login/register": {
            "post": {
                "tags": [
                    "Login"
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/User"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/User"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/User"
                            }
                        }
                    },
                    "required": true
                },
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            }
        },
        "/api/Registration/events/{eventId}": {
            "post": {
                "tags": [
                    "Registration"
                ],
                "parameters": [
                    {
                        "name": "eventId",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            },
            "delete": {
                "tags": [
                    "Registration"
                ],
                "parameters": [
                    {
                        "name": "eventId",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            }
        },
        "/api/Registration/user-registrations": {
            "get": {
                "tags": [
                    "Registration"
                ],
                "responses": {
                    "200": {
                        "description": "OK"
                    }
                }
            }
        },
        "/WeatherForecast": {
            "get": {
                "tags": [
                    "WeatherForecast"
                ],
                "operationId": "GetWeatherForecast",
                "responses": {
                    "200": {
                        "description": "OK",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/WeatherForecast"
                                    }
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/WeatherForecast"
                                    }
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/WeatherForecast"
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    },
    "components": {
        "schemas": {
            "CreateEventDto": {
                "required": [
                    "title",
                    "date",
                    "location",
                    "category"
                ],
                "type": "object",
                "properties": {
                    "title": {
                        "maxLength": 100,
                        "minLength": 0,
                        "type": "string"
                    },
                    "description": {
                        "maxLength": 1000,
                        "minLength": 0,
                        "type": "string"
                    },
                    "date": {
                        "type": "string",
                        "format": "date-time"
                    },
                    "location": {
                        "maxLength": 200,
                        "minLength": 0,
                        "type": "string"
                    },
                    "category": {
                        "maxLength": 50,
                        "minLength": 0,
                        "type": "string"
                    },
                    "capacity": {
                        "maximum": 10000,
                        "minimum": 1,
                        "type": "integer",
                        "format": "int32"
                    }
                }
            },
            "EventStatus": {
                "type": "integer"
            },
            "Role": {
                "type": "object",
                "properties": {
                    "id": {
                        "type": "integer",
                        "format": "int64"
                    },
                    "name": {
                        "type": "string"
                    },
                    "users": {
                        "type": "array",
                        "items": {
                            "$ref": "#/components/schemas/User"
                        }
                    }
                }
            },
            "UpdateEventDto": {
                "required": [
                    "title",
                    "date",
                    "location",
                    "category"
                ],
                "type": "object",
                "properties": {
                    "title": {
                        "maxLength": 100,
                        "minLength": 0,
                        "type": "string"
                    },
                    "description": {
                        "maxLength": 1000,
                        "minLength": 0,
                        "type": "string"
                    },
                    "date": {
                        "type": "string",
                        "format": "date-time"
                    },
                    "location": {
                        "maxLength": 200,
                        "minLength": 0,
                        "type": "string"
                    },
                    "category": {
                        "maxLength": 50,
                        "minLength": 0,
                        "type": "string"
                    },
                    "capacity": {
                        "maximum": 10000,
                        "minimum": 1,
                        "type": "integer",
                        "format": "int32"
                    }
                }
            },
            "UpdateEventStatusDto": {
                "required": [
                    "newStatus"
                ],
                "type": "object",
                "properties": {
                    "newStatus": {
                        "$ref": "#/components/schemas/EventStatus"
                    }
                }
            },
            "User": {
                "type": "object",
                "properties": {
                    "id": {
                        "type": "integer",
                        "format": "int64"
                    },
                    "username": {
                        "type": "string"
                    },
                    "password": {
                        "type": "string"
                    },
                    "roles": {
                        "type": "array",
                        "items": {
                            "$ref": "#/components/schemas/Role"
                        }
                    }
                }
            },
            "WeatherForecast": {
                "type": "object",
                "properties": {
                    "date": {
                        "type": "string",
                        "format": "date"
                    },
                    "temperatureC": {
                        "type": "integer",
                        "format": "int32"
                    },
                    "temperatureF": {
                        "type": "integer",
                        "format": "int32"
                    },
                    "summary": {
                        "type": "string",
                        "nullable": true
                    }
                }
            }
        }
    },
    "tags": [
        {
            "name": "Event"
        },
        {
            "name": "Login"
        },
        {
            "name": "Registration"
        },
        {
            "name": "WeatherForecast"
        }
    ]
}
```

## Brief explanation of my architecture decisions

## Controllers (HTTP Layer)

Single Responsibility: Handle only HTTP-related logic

Route incoming requests

Validate input models (DTOs)

Return proper HTTP status codes/responses

Thin Layer: No business logic - delegates everything to services

## Services (Business Logic Layer)

Core Rules: Contain all business logic and workflows

Data Access: Directly interact with DbContext (no repository)

Reusability: Shared across multiple controllers/endpoints

## What I would improve given more time

## Testing
 1. Implement unit tests

2. Add integration tests for API endpoints


## Error Handling

1. Standardize error responses across all endpoints

2. Implement global exception handling middleware


## Performance Optimizations

1. Add caching for frequently accessed data

2. Optimize database queries with proper indexing

3. Set up log aggregation
