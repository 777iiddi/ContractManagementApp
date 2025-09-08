import React, { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';
import { useNavigate } from 'react-router-dom';

export default function DashboardPage() {
    const { logout } = useAuth();
    const [contracts, setContracts] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchContracts = async () => {
            try {
                const response = await api.get('/Contrats');
                setContracts(response.data);
            } catch (error) {
                console.error("Erreur lors de la récupération des contrats", error);
                if (error.response?.status === 401) logout();
            } finally {
                setLoading(false);
            }
        };
        fetchContracts();
    }, [logout]);

    const handleRowClick = (contractId) => {
        navigate(`/contracts/${contractId}`);
    };

    if (loading) return <p>Chargement des contrats...</p>;

    return (
        <div>
            <div className="page-header">
                <h1>Tableau de Bord RH</h1>
            </div>
            <div className="table-container">
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
                                <td colSpan="5" style={{ textAlign: 'center' }}>Aucun contrat à afficher.</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
