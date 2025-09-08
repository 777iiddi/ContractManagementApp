// FICHIER À MODIFIER : frontend/src/pages/ManagerDashboardPage.jsx

import React, { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify'; // Il est bon d'importer toast pour la gestion d'erreurs

export default function ManagerDashboardPage() {
    const { logout } = useAuth();
    const [contracts, setContracts] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    // --- DÉBUT DE LA CORRECTION ---
    useEffect(() => {
        const fetchContractsForValidation = async () => {
            try {
                // On appelle l'endpoint spécifique pour les managers
                const response = await api.get('/Contrats/validation');
                setContracts(response.data);
            } catch (error) {
                console.error("Erreur lors de la récupération des contrats à valider", error);
                toast.error("Impossible de charger les contrats à valider.");
                if (error.response?.status === 401) logout();
            } finally {
                // Très important : on arrête le chargement que la requête réussisse ou échoue
                setLoading(false);
            }
        };
        fetchContractsForValidation();
    }, [logout]);
    // --- FIN DE LA CORRECTION ---


    const handleRowClick = (contractId) => {
        navigate(`/contracts/${contractId}`);
    };

    if (loading) return <p>Chargement des contrats à valider...</p>;

    return (
        <div>
            <div className="page-header">
                <h1>Tableau de Bord Manager</h1>
                <button onClick={() => navigate('/employees/new')} style={{backgroundColor: 'var(--primary-color)', color: 'white'}}>
                    Ajouter un Employé
                </button>
            </div>
            <div className="table-container">
                <h2>Contrats en attente de validation</h2>
                <table>
                    <thead>
                        <tr>
                            <th>Référence</th>
                            <th>Employé</th>
                            <th>Type</th>
                            <th>Statut</th>
                            <th>Date de début</th>
                        </tr>
                    </thead>
                    <tbody>
                        {contracts.length > 0 ? (
                            contracts.map(contract => (
                                <tr key={contract.id} onClick={() => handleRowClick(contract.id)}>
                                    <td>{contract.reference}</td>
                                    <td>{contract.nomEmploye}</td>
                                    <td>{contract.typeDeContrat}</td>
                                    <td>{contract.statut}</td>
                                    <td>{new Date(contract.dateDebut).toLocaleDateString()}</td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan="5" style={{ textAlign: 'center' }}>Aucun contrat à valider.</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
}