/* Dev Environment */

import { Environment } from "./env-interface";

const env: Environment = {
  apiUrl: 'http://localhost:50695/',
  auth: {
    authority: 'http://localhost:5000/',
    client_id: 'dna-app-client-dev',
    redirect_uri: 'http://localhost:3333/',
    changePasswordUrl: 'https://localhost:44354/account/changepassword'
  },
  apiTawkTo: '5bdb0902a3e36b12bc6ea6b5'
}

export default env;
