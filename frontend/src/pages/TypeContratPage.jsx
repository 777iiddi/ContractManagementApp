import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import { toast } from 'react-toastify';

export default function TypeContratsPage() {
    const [typeContrats, setTypeContrats] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        fetchTypeContrats();
    }, []);

    const fetchTypeContrats = async () => {
        try {
            const response = await api.get('/TypeContrats');
            setTypeContrats(response.data);
        } catch (error) {
            console.error('Erreur lors du chargement des types de contrat', error);
            toast.error('Erreur lors du chargement');
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <div>Chargement...</div>;

    return (
        <div style={{ padding: '20px' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
                <h1>Gestion des Types de Contrats</h1>
                <button 
                    onClick={() => navigate('/type-contrats/nouveau')}
                    style={{
                        backgroundColor: '#007bff',
                        color: 'white',
                        padding: '10px 20px',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer'
                    }}
                >
                    Nouveau Type de Contrat
                </button>
            </div>

            {typeContrats.length > 0 ? (
                <table style={{ width: '100%', borderCollapse: 'collapse', border: '1px solid #ddd' }}>
                    <thead>
                        <tr style={{ backgroundColor: '#f8f9fa' }}>
                            <th style={{ padding: '12px', border: '1px solid #ddd', textAlign: 'left' }}>Nom</th>
                            <th style={{ padding: '12px', border: '1px solid #ddd', textAlign: 'left' }}>Description</th>
                            <th style={{ padding: '12px', border: '1px solid #ddd', textAlign: 'center' }}>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {typeContrats.map(typeContrat => (
                            <tr key={typeContrat.id}>
                                <td style={{ padding: '12px', border: '1px solid #ddd' }}>
                                    {typeContrat.nom}
                                </td>
                                <td style={{ padding: '12px', border: '1px solid #ddd' }}>
                                    {typeContrat.description || 'Aucune description'}
                                </td>
                                <td style={{ padding: '12px', border: '1px solid #ddd', textAlign: 'center' }}>
                                    <button
                                        onClick={() => console.log('Modifier', typeContrat.id)}
                                        style={{
                                            backgroundColor: '#28a745',
                                            color: 'white',
                                            padding: '4px 8px',
                                            border: 'none',
                                            borderRadius: '4px',
                                            cursor: 'pointer'
                                        }}
                                    >
                                        Modifier
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            ) : (
                <div style={{ textAlign: 'center', padding: '40px' }}>
                    <p>Aucun type de contrat configuré</p>
                    <button 
                        onClick={() => navigate('/type-contrats/nouveau')}
                        style={{
                            backgroundColor: '#007bff',
                            color: 'white',
                            padding: '10px 20px',
                            border: 'none',
                            borderRadius: '4px',
                            cursor: 'pointer'
                        }}
                    >
                        Créer le premier type de contrat
                    </button>
                </div>
            )}
        </div>
    );
}
