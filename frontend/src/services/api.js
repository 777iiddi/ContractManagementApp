import axios from 'axios';

// On crée une instance d'axios avec l'URL de base de notre API.
// Assurez-vous que le port (5286) correspond bien à celui de votre projet backend.
const api = axios.create({
    baseURL: 'http://localhost:5286/api', 
});

// On configure un "intercepteur" qui s'exécutera avant chaque requête.
// Son rôle est de récupérer le token depuis le localStorage et de l'ajouter dans les en-têtes.
// C'est ce qui va résoudre l'erreur 401 Unauthorized.
api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('authToken');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

export default api;
