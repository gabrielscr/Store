/* Production Environment */

import { Environment } from "./env-interface";

const env: Environment = {
  apiUrl: 'https://tempus-dna-app-hom.azurewebsites.net/',
  auth: {
    authority: 'https://tempus-dna-login-hom.azurewebsites.net/',
    client_id: 'dna-app-client-hom',
    redirect_uri: 'https://tempus-dna-app-hom.azurewebsites.net/',
    changePasswordUrl: 'https://tempus-dna-login-hom.azurewebsites.net/account/changepassword'
  },
  apiTawkTo: '5bdb0902a3e36b12bc6ea6b5'
}

export default env;
