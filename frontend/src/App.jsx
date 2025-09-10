import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link, Navigate, Outlet } from 'react-router-dom';
import { AuthProvider, useAuth } from './context/AuthContext';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

// --- Pages ---
import TypeContratsPage from './pages/TypeContratPage';
import NewTypeContratPage from './pages/NewTypeContratPage';
import LoginPage from './pages/LoginPage';
import DashboardPage from './pages/DashboardPage';
import ManagerDashboardPage from './pages/ManagerDashboardPage';
import NewContractPage from './pages/NewContractPage';
import ContractDetailPage from './pages/ContractDetailPage';
import AdminPage from './pages/AdminPage';
 // Importer la nouvelle page

// --- Composants ---
import ProtectedRoute from './components/ProtectedRoute';

// Le composant Layout gère la structure visuelle et la navigation
function Layout() {
    const { isAuthenticated, logout, userRole } = useAuth();

    if (!isAuthenticated) {
        return <Outlet />; // Affiche LoginPage ou autre route publique
    }

    return (
        <>
            <header className="main-header">
                <nav className="main-nav">
                    <Link to="/">Accueil</Link>
                    {userRole === 'RH' && <Link to="/dashboard-rh">Tableau de Bord RH</Link>}
                    {userRole === 'Manager' && <Link to="/dashboard-manager">Tableau de Bord Manager</Link>}
                    {userRole === 'Admin' && <Link to="/admin">Administration</Link>}
                    
                    {/* AJOUT : Lien vers la gestion des types de contrat */}
                    {(userRole === 'Admin' || userRole === 'Manager') && (
                        <Link to="/types-contrat">Gérer les Types</Link>
                    )}
                </nav>
                <button onClick={logout} className="button-logout">Déconnexion</button>
            </header>
            <main className="main-content">
                <Outlet /> {/* C'est ici que les pages protégées seront affichées */}
            </main>
        </>
    );
}

// Le composant HomePage gère la redirection après la connexion
function HomePage() {
    const { userRole } = useAuth();

    if (userRole === 'Admin') {
        return <Navigate to="/admin" replace />;
    }
    if (userRole === 'Manager') {
        return <Navigate to="/dashboard-manager" replace />;
    }
    // Par défaut, ou si le rôle est RH
    return <Navigate to="/dashboard-rh" replace />;
}


function App() {
    return (
        <AuthProvider>
            <Router>
                <ToastContainer position="top-right" autoClose={5000} hideProgressBar={false} />
                <Routes>
                    <Route element={<Layout />}>
                        <Route path="/login" element={<LoginPage />} />
                        
                        {/* Routes protégées */}
                        <Route element={<ProtectedRoute />}>
                            <Route path="/" element={<HomePage />} />
                            <Route path="/dashboard-rh" element={<DashboardPage />} />
                            <Route path="/dashboard-manager" element={<ManagerDashboardPage />} />
                            <Route path="/admin" element={<AdminPage />} />
                            <Route path="/contracts/new" element={<NewContractPage />} />
                            <Route path="/contracts/:contractId" element={<ContractDetailPage />} />
                            <Route path="/type-contrats" element={<ProtectedRoute><TypeContratsPage /></ProtectedRoute>} />
                            <Route path="/type-contrats/nouveau" element={<ProtectedRoute><NewTypeContratPage /></ProtectedRoute>} />
                        </Route>
                    </Route>
                </Routes>
            </Router>
        </AuthProvider>
    );
}

export default App;