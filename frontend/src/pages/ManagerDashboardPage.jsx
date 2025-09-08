import React, { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';
import { useNavigate } from 'react-router-dom';

export default function ManagerDashboardPage() {
    const { logout } = useAuth();
    const [contracts, setContracts] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        // ... (la logique de fetch ne change pas)
    }, [logout]);

    const handleRowClick = (contractId) => {
        navigate(`/contracts/${contractId}`);
    };

    if (loading) return <p>Chargement des contrats à valider...</p>;

    return (
        <div>
            <div className="page-header">
                <h1>Tableau de Bord Manager</h1>
                {/* AJOUT : Bouton pour créer un nouvel employé */}
                <button onClick={() => navigate('/employees/new')} style={{backgroundColor: 'var(--primary-color)', color: 'white'}}>
                    Ajouter un Employé
                </button>
            </div>
            <div className="table-container">
                <h2>Contrats en attente de validation</h2>
                <table>
                    {/* ... (le tableau ne change pas) ... */}
                </table>
            </div>
        </div>
    );
}
