export interface Environment {
  /** URL da API de dados, incluindo a barra no final */
  apiUrl: string;

  /** Dados sobre a autenticacao */
  auth: {
    /** URL do site de login, incluindo a barra no final */
    authority: string,

    /** client_id deste aplicativo */
    client_id: string,

    /** URL para a qual o site de login deve redirecionar */
    redirect_uri: string,

    /** URL para alterar senha */
    changePasswordUrl: string
  };

  apiTawkTo: string;
}
