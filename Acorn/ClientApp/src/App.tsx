import { Container } from "@mui/material";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Layout } from "./Layout";
import { Category } from "./pages/Category";

function App() {
  return (
    <Container>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route path={'category?/:id?'} element={<Category />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </Container>
  );
}

export default App;
