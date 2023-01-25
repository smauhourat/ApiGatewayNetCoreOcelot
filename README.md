## Test ApiGateway with Ocelot



### TestApiGateway
	This project expose three end points:
	
	1. **api/posts**
	2. **api/users**
	3. **api/userPosts**

1 and 2, are direct call to others microservices, and 3 is a compose or aggregate calls

### TokenGenerator
	This projects is only for generate a JWT Token for api authentication tests