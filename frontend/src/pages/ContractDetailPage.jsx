import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';
import { useAuth } from '../context/AuthContext';
import { toast } from 'react-toastify';

export default function ContractDetailPage() {
    const { contractId } = useParams();
    const navigate = useNavigate();
    const { userRole } = useAuth();
    
    const [contract, setContract] = useState(null);
    const [loading, setLoading] = useState(true);
    const [commentaire, setCommentaire] = useState('');
    const [actionLoading, setActionLoading] = useState(false);

    useEffect(() => {
        const fetchContractDetails = async () => {
            try {
                const response = await api.get(`/Contrats/${contractId}`);
                setContract(response.data);
            } catch (error) {
                console.error("Erreur lors de la récupération des détails du contrat", error);
                toast.error("Contrat introuvable.");
                navigate('/');
            } finally {
                setLoading(false);
            }
        };
        fetchContractDetails();
    }, [contractId, navigate]);

    // NOUVELLE FONCTION POUR APPROUVER
    const handleApprove = async () => {
        if (actionLoading) return;
        setActionLoading(true);

        try {
            await api.post(`/Contrats/${contractId}/approve`, JSON.stringify(commentaire), {
                headers: { 'Content-Type': 'application/json' }
            });
            toast.success('Contrat approuvé avec succès !');
            navigate('/dashboard-manager');
        } catch (error) {
            console.error('Erreur lors de l\'approbation du contrat', error);
            toast.error('Échec de l\'approbation.');
        } finally {
            setActionLoading(false);
        }
    };

    // NOUVELLE FONCTION POUR REJETER
    const handleReject = async () => {
        if (!commentaire.trim()) {
            toast.error("Un commentaire est requis pour rejeter un contrat.");
            return;
        }

        if (actionLoading) return;
        setActionLoading(true);

        try {
            await api.post(`/Contrats/${contractId}/reject`, JSON.stringify(commentaire), {
                headers: { 'Content-Type': 'application/json' }
            });
            toast.success('Contrat rejeté avec succès !');
            navigate('/dashboard-manager');
        } catch (error) {
            console.error('Erreur lors du rejet du contrat', error);
            toast.error('Échec du rejet.');
        } finally {
            setActionLoading(false);
        }
    };

    if (loading) return <div>Chargement des détails...</div>;
    if (!contract) return <div>Contrat non trouvé.</div>;

    const canValidate = userRole === 'Manager' && contract.statut === 'En validation';

    return (
        <div>
            <button onClick={() => navigate('/dashboard-manager')}>
                ← Retour au tableau de bord
            </button>
            
            <div>
                <h1>Détails du Contrat : {contract.reference}</h1>
                
                <div>
                    <h3>Informations Générales</h3>
                    <p><strong>Statut :</strong> {contract.statut}</p>
                    <p><strong>Employé :</strong> {contract.nomEmploye}</p>
                    <p><strong>Type de contrat :</strong> {contract.typeDeContrat}</p>
                    <p><strong>Société :</strong> {contract.nomSociete}</p>
                    <p><strong>Date de début :</strong> {new Date(contract.dateDebut).toLocaleDateString()}</p>
                    {contract.dateFin && (
                        <p><strong>Date de fin :</strong> {new Date(contract.dateFin).toLocaleDateString()}</p>
                    )}
                </div>

                {canValidate && (
                    <div>
                        <h3>Actions de Validation</h3>
                        <div>
                            <label>Commentaire</label>
                            <textarea
                                value={commentaire}
                                onChange={(e) => setCommentaire(e.target.value)}
                                placeholder="Ajouter un commentaire (requis pour le rejet)"
                                rows="4"
                                cols="50"
                            />
                        </div>
                        <div>
                            <button 
                                onClick={handleApprove}
                                disabled={actionLoading}
                                style={{
                                    backgroundColor: '#28a745',
                                    color: 'white',
                                    padding: '10px 20px',
                                    margin: '5px',
                                    border: 'none',
                                    borderRadius: '4px',
                                    cursor: actionLoading ? 'not-allowed' : 'pointer'
                                }}
                            >
                                {actionLoading ? 'Traitement...' : 'Valider'}
                            </button>
                            <button 
                                onClick={handleReject}
                                disabled={actionLoading}
                                style={{
                                    backgroundColor: '#dc3545',
                                    color: 'white',
                                    padding: '10px 20px',
                                    margin: '5px',
                                    border: 'none',
                                    borderRadius: '4px',
                                    cursor: actionLoading ? 'not-allowed' : 'pointer'
                                }}
                            >
                                {actionLoading ? 'Traitement...' : 'Rejeter'}
                            </button>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}
