import { HttpClientService } from './http-client';
import { getToken, authService } from './auth-service';
import env from '../env/env';

let apiService = new HttpClientService();

apiService
  .configure(c => c
    .useStandardConfiguration()
    .withBaseUrl(env.apiUrl)
    .withInterceptor({
      request(request) {
        let user = getToken();
        if(user) {
          request.headers.append('Authorization', `Bearer ${user.access_token}`);
        }

        return request;
      },
      responseError(error) {
        if(error.status === 401)
        {
          authService.signinRedirect();
          return error;
        }
        else
          throw error;
      }
    })
    .rejectErrorResponses()
  );

  
export { apiService };
