import { Link, Routes, Route, Navigate } from 'react-router-dom';
import { useAuth } from './context/AuthContext';
import LoginPage from './pages/LoginPage';
import ProtectedRoute from './components/ProtectedRoute';
import HomePage from './pages/HomePage';
import NewContractPage from './pages/NewContractPage';
import ContractDetailPage from './pages/ContractDetailPage';
import AdminPage from './pages/AdminPage';

function Layout() {
    const { isAuthenticated, logout, userRole } = useAuth();
    return (
        <>
            {isAuthenticated && (
                <nav>
                    <Link to="/">Tableau de bord</Link>
                    {(userRole === 'RH' || userRole === 'Admin') && (
                        <> | <Link to="/contracts/new">Nouveau Contrat</Link></>
                    )}
                    {userRole === 'Admin' && (
                        <> | <Link to="/admin">Administration</Link></>
                    )}
                    <button onClick={logout} style={{ marginLeft: '20px' }}>Déconnexion</button>
                </nav>
            )}
            <main>
                <Routes>
                    <Route path="/login" element={<LoginPage />} />
                    <Route element={<ProtectedRoute />}>
                        <Route path="/" element={<HomePage />} />
                        <Route path="/contracts/new" element={<NewContractPage />} />
                        <Route path="/contracts/:contractId" element={<ContractDetailPage />} />
                        <Route path="/admin" element={<AdminPage />} />
                        <Route path="*" element={<Navigate to="/" />} />
                    </Route>
                </Routes>
            </main>
        </>
    );
}
