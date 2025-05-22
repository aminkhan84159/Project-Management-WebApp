export interface IProject {
    projectId: number; 
    projectName: string; 
    description: string; 
    dueDate:Date;
    statusId: number; 
    isActive: boolean; 
    createdBy:number;
}