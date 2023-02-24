import { Container } from "@mui/material";
import { Outlet } from "react-router-dom";

export function Layout() {
  return (
    <Container>
      {/* todo: add header here */}
      <Outlet />
      {/* todo: add footer here */}
    </Container>
  );
}
