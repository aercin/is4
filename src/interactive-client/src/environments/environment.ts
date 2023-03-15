//Development Environment  ng build -env=dev | ng serve -e dev
export const environment = {
    production: false,
    idp_base_url: "http://localhost:5080",
    bff_base_url: "http://localhost:5205",//resource api base url
    // bff_base_url: "http://localhost:57981",//legacy resource api base url
    client_id: "interactive-client",
    client_secret: "interactive", 
  }; 
  