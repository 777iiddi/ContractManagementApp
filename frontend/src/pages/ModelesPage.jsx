import React, { useState, useEffect } from 'react';
import api from '../services/api';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

export default function ModelesPage() {
    const [modeles, setModeles] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchModeles = async () => {
            try {
                const response = await api.get('/Modeles');
                setModeles(response.data);
            } catch (error) {
                toast.error("Impossible de charger les modèles de documents.");
            } finally {
                setLoading(false);
            }
        };
        fetchModeles();
    }, []);

    if (loading) return <p>Chargement des modèles...</p>;

    return (
        <div>
            <div className="page-header">
                <h1>Gestion des Modèles de Documents</h1>
                <button onClick={() => navigate('/modeles/new')}>Créer un Modèle</button>
            </div>
            <div className="table-container">
                <table>
                    <thead>
                        <tr>
                            <th>Nom du Modèle</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {modeles.map(modele => (
                            <tr key={modele.id} onClick={() => navigate(`/modeles/${modele.id}`)}>
                                <td>{modele.nom}</td>
                                <td>
                                    <button>Modifier</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
