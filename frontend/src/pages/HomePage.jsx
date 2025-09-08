import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

// Ce composant est la première page vue après la connexion.
// Son seul rôle est de rediriger vers le bon tableau de bord.
export default function HomePage() {
    const { userRole } = useAuth();

    if (userRole === 'Manager') {
        return <Navigate to="/dashboard-manager" replace />;
    }
    
    // Par défaut (pour RH, Admin, etc.)
    return <Navigate to="/dashboard-rh" replace />;
}
