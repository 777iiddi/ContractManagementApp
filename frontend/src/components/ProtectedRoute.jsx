import React from 'react';
import { useAuth } from '../context/AuthContext';
import { Navigate, Outlet } from 'react-router-dom';

// Ce composant est un "garde".
// Si l'utilisateur est connecté, il affiche la page demandée (Outlet).
// Sinon, il le redirige vers la page de connexion.
export default function ProtectedRoute() {
    const { isAuthenticated } = useAuth();

    return isAuthenticated ? <Outlet /> : <Navigate to="/login" replace />;
}