/* Production Environment */

import { Environment } from "./env-interface";

const env: Environment = {
  apiUrl: 'https://app.dnacidadania.com.br/',
  auth: {
    authority: 'https://login.dnacidadania.com.br/',
    client_id: 'dna-app-client-prod',
    redirect_uri: 'https://app.dnacidadania.com.br/',
    changePasswordUrl: 'https://login.dnacidadania.com.br/account/changepassword'
  },
  apiTawkTo: '5c773205a726ff2eea59d0aa'
}

export default env;
