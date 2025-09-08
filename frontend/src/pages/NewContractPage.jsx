import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import { toast } from 'react-toastify';

export default function NewContractPage() {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        reference: `CONTRAT-${Date.now()}`,
        dateDebut: '',
        dateFin: null,
        employeId: '', // On initialise à vide
        typeContratId: '',
        societeId: '',
    });
    
    // États pour stocker les listes
    const [employes, setEmployes] = useState([]);
    const [typeContrats, setTypeContrats] = useState([]);
    const [societes, setSocietes] = useState([]);
    const [loading, setLoading] = useState(true);

    // On charge les listes au démarrage du composant
    useEffect(() => {
        const fetchDropdownData = async () => {
            try {
                const [employesRes, typeContratsRes, societesRes] = await Promise.all([
                    api.get('/Employes'),
                    api.get('/TypeContrats'),
                    api.get('/Societes')
                ]);
                setEmployes(employesRes.data);
                setTypeContrats(typeContratsRes.data);
                setSocietes(societesRes.data);
            } catch (err) {
                toast.error("Erreur lors du chargement des données de configuration.");
            } finally {
                setLoading(false);
            }
        };
        fetchDropdownData();
    }, []);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await api.post('/Contrats', {
                ...formData,
                employeId: parseInt(formData.employeId),
                typeContratId: parseInt(formData.typeContratId),
                societeId: parseInt(formData.societeId),
                dateFin: formData.dateFin || null,
            });
            toast.success('Contrat créé avec succès !');
            navigate('/');
        } catch (err) {
            toast.error('Erreur lors de la création du contrat.');
        }
    };

    if (loading) {
        return <p>Chargement du formulaire...</p>;
    }

    return (
        <div>
            <h2>Créer un Nouveau Contrat</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Référence</label>
                    <input name="reference" value={formData.reference} onChange={handleChange} required />
                </div>
                <div>
                    <label>Date de Début</label>
                    <input name="dateDebut" type="date" onChange={handleChange} required />
                </div>
                <div>
                    <label>Date de Fin (Optionnel)</label>
                    <input name="dateFin" type="date" onChange={handleChange} />
                </div>
                
                {/* Listes déroulantes */}
                <div>
                    <label>Employé</label>
                    <select name="employeId" value={formData.employeId} onChange={handleChange} required>
                        <option value="">Sélectionnez un employé</option>
                        {employes.map(e => <option key={e.id} value={e.id}>{e.nomComplet}</option>)}
                    </select>
                </div>
                <div>
                    <label>Type de Contrat</label>
                    <select name="typeContratId" value={formData.typeContratId} onChange={handleChange} required>
                        <option value="">Sélectionnez un type</option>
                        {typeContrats.map(t => <option key={t.id} value={t.id}>{t.nom}</option>)}
                    </select>
                </div>
                <div>
                    <label>Société</label>
                    <select name="societeId" value={formData.societeId} onChange={handleChange} required>
                        <option value="">Sélectionnez une société</option>
                        {societes.map(s => <option key={s.id} value={s.id}>{s.nom}</option>)}
                    </select>
                </div>

                <button type="submit">Créer</button>
            </form>
        </div>
    );
}
