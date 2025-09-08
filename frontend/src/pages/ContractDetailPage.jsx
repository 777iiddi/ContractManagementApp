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

    const handleAction = async (action) => {
        const etapeEnAttente = contract?.workflow?.etapes.find(e => e.statut === 'En attente');
        if (!etapeEnAttente) {
            toast.warn("Aucune étape en attente de validation pour ce contrat.");
            return;
        }

        if (action === 'rejeter' && !commentaire) {
            toast.error("Un commentaire est requis pour rejeter un contrat.");
            return;
        }

        try {
            await api.post(`/Workflows/etapes/${etapeEnAttente.id}/${action}`, `"${commentaire}"`, {
                headers: { 'Content-Type': 'application/json' }
            });
            toast.success(`Contrat ${action === 'valider' ? 'validé' : 'rejeté'} avec succès !`);
            navigate('/dashboard-manager');
        } catch (error) {
            console.error(`Erreur lors de la ${action} du contrat`, error);
            toast.error(`Échec de la ${action}.`);
        }
    };

    if (loading) return <p>Chargement des détails...</p>;
    if (!contract) return <p>Contrat non trouvé.</p>;

    const canValidate = userRole === 'Manager' && contract.statut === 'En validation';

    return (
        <div>
            <div className="page-header">
                <h1>Détails du Contrat : {contract.reference}</h1>
            </div>

            <div className="details-container">
                <h3>Informations Générales</h3>
                <p><strong>Statut :</strong> {contract.statut}</p>
                <p><strong>Employé :</strong> {contract.nomEmploye}</p>
                <p><strong>Type de contrat :</strong> {contract.typeDeContrat}</p>
                <p><strong>Société :</strong> {contract.nomSociete}</p>
                <p><strong>Date de début :</strong> {new Date(contract.dateDebut).toLocaleDateString()}</p>
                {contract.dateFin && <p><strong>Date de fin :</strong> {new Date(contract.dateFin).toLocaleDateString()}</p>}
            </div>

            {canValidate && (
                <div className="validation-section">
                    <h3>Actions de Validation</h3>
                    <div>
                        <label htmlFor="commentaire">Commentaire</label>
                        <textarea 
                            id="commentaire"
                            value={commentaire}
                            onChange={(e) => setCommentaire(e.target.value)}
                            placeholder="Ajouter un commentaire (requis pour le rejet)"
                        />
                    </div>
                    <div className="action-buttons">
                        <button onClick={() => handleAction('valider')} className="validate-btn">Valider</button>
                        <button onClick={() => handleAction('rejeter')} className="reject-btn">Rejeter</button>
                    </div>
                </div>
            )}
        </div>
    );
}
