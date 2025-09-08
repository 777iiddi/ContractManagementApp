import React, { createContext, useState, useContext } from 'react';
import api from '../services/api';

const AuthContext = createContext(null);

// Fonction pour décoder le token JWT
const parseJwt = (token) => {
    try {
        return JSON.parse(atob(token.split('.')[1]));
    } catch (e) {
        return null;
    }
};

export const AuthProvider = ({ children }) => {
    const [token, setToken] = useState(localStorage.getItem('authToken'));
    const [userRole, setUserRole] = useState(localStorage.getItem('userRole'));

    const login = async (email, password) => {
        const response = await api.post('/Auth/login', { email, password });
        const newToken = response.data.token;
        const decodedToken = parseJwt(newToken);
        
        const role = decodedToken?.role || decodedToken?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

        // --- AJOUT POUR LE DÉBOGAGE ---
        console.log("Rôle extrait du token JWT :", role);

        setToken(newToken);
        setUserRole(role);
        localStorage.setItem('authToken', newToken);
        localStorage.setItem('userRole', role);
    };

    const logout = () => {
        setToken(null);
        setUserRole(null);
        localStorage.removeItem('authToken');
        localStorage.removeItem('userRole');
    };

    const value = {
        token,
        userRole,
        login,
        logout,
        isAuthenticated: !!token,
    };

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
    return useContext(AuthContext);
};
