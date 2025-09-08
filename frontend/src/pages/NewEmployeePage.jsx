// FICHIER À MODIFIER : frontend/src/pages/NewEmployeePage.jsx

import React, { useState, useEffect } from 'react'; // Importer useEffect
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import { toast } from 'react-toastify';

export default function NewEmployeePage() {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        matricule: '',
        nom: '',
        prenom: '',
        managerId: '', // <-- AJOUT : Champ pour l'ID du manager
    });
    
    // --- AJOUTS CI-DESSOUS ---
    const [managers, setManagers] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchManagers = async () => {
            try {
                // On récupère tous les utilisateurs et on filtrera côté client
                const response = await api.get('/Auth'); 
                // On garde seulement les utilisateurs avec le rôle "Manager"
                setManagers(response.data.filter(user => user.role === 'Manager'));
            } catch (error) {
                toast.error("Impossible de charger la liste des managers.");
            } finally {
                setLoading(false);
            }
        };
        fetchManagers();
    }, []);
    // --- FIN DES AJOUTS ---

    const [error, setError] = useState('');

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        try {
            // On prépare les données à envoyer
            const submissionData = {
                ...formData,
                // On s'assure que managerId est un nombre ou null
                managerId: formData.managerId ? parseInt(formData.managerId) : null,
            };
            
            await api.post('/Employes', submissionData); // On envoie les données complètes
            toast.success('Employé créé avec succès !');
            navigate('/'); 
        } catch (err) {
            console.error(err);
            toast.error(err.response?.data?.message || "Erreur lors de la création de l'employé.");
            setError('Erreur lors de la création de l\'employé.');
        }
    };

    if (loading) {
        return <p>Chargement du formulaire...</p>;
    }

    return (
        <div>
            <div className="page-header">
                <h1>Créer un Nouvel Employé</h1>
            </div>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="matricule">Matricule</label>
                    <input id="matricule" name="matricule" value={formData.matricule} onChange={handleChange} required />
                </div>
                <div>
                    <label htmlFor="nom">Nom</label>
                    <input id="nom" name="nom" value={formData.nom} onChange={handleChange} required />
                </div>
                <div>
                    <label htmlFor="prenom">Prénom</label>
                    <input id="prenom" name="prenom" value={formData.prenom} onChange={handleChange} required />
                </div>

                {/* --- AJOUT DE LA LISTE DÉROULANTE --- */}
                <div>
                    <label htmlFor="managerId">Manager</label>
                    <select id="managerId" name="managerId" value={formData.managerId} onChange={handleChange}>
                        <option value="">Aucun manager</option>
                        {managers.map(manager => (
                            <option key={manager.id} value={manager.id}>
                                {manager.email}
                            </option>
                        ))}
                    </select>
                </div>

                <button type="submit">Créer l'employé</button>
                {error && <p style={{ color: 'red' }}>{error}</p>}
            </form>
        </div>
    );
}