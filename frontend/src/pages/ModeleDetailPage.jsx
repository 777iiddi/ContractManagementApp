import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';
import { toast } from 'react-toastify';

export default function ModeleDetailPage() {
    const { modeleId } = useParams();
    const navigate = useNavigate();
    const isNew = modeleId === 'new';

    const [nom, setNom] = useState('');
    const [contenu, setContenu] = useState('');

    useEffect(() => {
        if (!isNew) {
            // Cette logique chargera les données d'un modèle existant.
            // Pour l'instant, nous nous concentrons sur la création.
            // api.get(`/Modeles/${modeleId}`).then(res => { setNom(res.data.nom); setContenu(res.data.contenuTemplate); });
        }
    }, [modeleId, isNew]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const command = {
                id: isNew ? 0 : parseInt(modeleId),
                nom: nom,
                contenuTemplate: contenu
            };
            await api.post('/Modeles', command);
            toast.success(`Modèle ${isNew ? 'créé' : 'mis à jour'} avec succès !`);
            navigate('/modeles');
        } catch (error) {
            toast.error("Erreur lors de la sauvegarde du modèle.");
        }
    };

    return (
        <div>
            <div className="page-header">
                <h1>{isNew ? 'Créer un Nouveau Modèle' : 'Modifier le Modèle'}</h1>
            </div>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="nom">Nom du Modèle</label>
                    <input id="nom" value={nom} onChange={(e) => setNom(e.target.value)} required />
                </div>
                <div style={{gridColumn: '1 / -1'}}>
                    <label htmlFor="contenu">Contenu du Modèle (avec variables comme `{{NomEmploye}}`)</label>
                    <textarea 
                        id="contenu"
                        value={contenu}
                        onChange={(e) => setContenu(e.target.value)}
                        rows="15"
                        required
                    />
                </div>
                <button type="submit">Sauvegarder</button>
            </form>
        </div>
    );
}
