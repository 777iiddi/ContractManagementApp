import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import { toast } from 'react-toastify';

export default function NewEmployeePage() {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        matricule: '',
        nom: '',
        prenom: '',
    });
    const [error, setError] = useState('');

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        try {
            await api.post('/Employes', formData);
            toast.success('Employé créé avec succès !');
            navigate('/'); // Redirige vers le tableau de bord
        } catch (err) {
            console.error(err);
            toast.error(err.response?.data?.message || "Erreur lors de la création de l'employé.");
            setError('Erreur lors de la création de l\'employé.');
        }
    };

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
                <button type="submit">Créer l'employé</button>
                {error && <p style={{ color: 'red' }}>{error}</p>}
            </form>
        </div>
    );
}
